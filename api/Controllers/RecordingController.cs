using api.Data;
using api.Data.Entities;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController, Route("recordings")]
    public class RecordingController : Controller
    {
        private readonly IWhisperClient _whisperClient;
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ILogger<RecordingController> _logger;
        private readonly string _recordingsDirectory;

        public RecordingController(
            IWhisperClient whisperClient,
            AppDbContext dbContext,
            IConfiguration config,
            ILogger<RecordingController> logger)
        {
            _whisperClient = whisperClient;
            _dbContext = dbContext;
            _config = config;
            _logger = logger;

            var configuredPath = _config["RECORDINGS_PATH"];
            if (!string.IsNullOrWhiteSpace(configuredPath))
            {
                _recordingsDirectory = configuredPath;
            }
            else if (OperatingSystem.IsWindows() && !Directory.Exists("/var/recordings"))
            {
                _recordingsDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "data"));
            }
            else
            {
                _recordingsDirectory = Path.Combine("/var", "recordings");
            }

            Directory.CreateDirectory(_recordingsDirectory);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            IFormFile file,
            [FromQuery] bool transcribe = false,
            [FromQuery] string? language = null,
            [FromQuery] string? model = null,
            CancellationToken ct = default)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No audio file provided.");

            var id = Guid.CreateVersion7();
            var ext = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(ext))
                ext = ".mp3";

            var audioFileName = $"{id}{ext}";
            var audioFilePath = Path.Combine(_recordingsDirectory, audioFileName);

            try
            {
                await using (var destination = new FileStream(
                    audioFilePath,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 81920,
                    useAsync: true
                ))
                {
                    await file.CopyToAsync(destination, ct);
                }

                _logger.LogInformation("Audio file saved: {Path} ({Size} bytes)", audioFilePath, file.Length);

                if (!transcribe)
                {
                    return Ok(new
                    {
                        id,
                        fileName = audioFileName,
                        size = file.Length,
                        filePath = audioFilePath
                    });
                }

                // Transcribe immediately
                try
                {
                    await using var readStream = System.IO.File.OpenRead(audioFilePath);
                    var transcriptResult = await _whisperClient.TranscribeAudioAsync(
                        readStream,
                        file.FileName,
                        language,
                        model,
                        ct
                    );

                    var textFileName = $"{id}.txt";
                    var textFilePath = Path.Combine(_recordingsDirectory, textFileName);
                    await System.IO.File.WriteAllTextAsync(textFilePath, transcriptResult.Text, ct);

                    return Ok(new
                    {
                        id,
                        fileName = audioFileName,
                        size = file.Length,
                        filePath = audioFilePath,
                        transcriptPath = textFilePath,
                        transcript = transcriptResult.Text,
                        details = transcriptResult
                    });
                }
                catch (WhisperException ex)
                {
                    _logger.LogError(ex, "Whisper transcription failed during upload: {Message}", ex.Message);
                    return StatusCode(ex.StatusCode ?? StatusCodes.Status502BadGateway, new
                    {
                        id,
                        fileName = audioFileName,
                        size = file.Length,
                        filePath = audioFilePath,
                        error = ex.Message
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload file {FileName}", file.FileName);
                if (System.IO.File.Exists(audioFilePath) && !transcribe)
                {
                    try { System.IO.File.Delete(audioFilePath); } catch { /* ignore */ }
                }

                throw;
            }
        }

        [HttpPost("{id}/transcribe")]
        public async Task<IActionResult> Transcribe(
            string id,
            [FromQuery] string? language = null,
            [FromQuery] string? model = null,
            CancellationToken ct = default)
        {
            var audioFile = Directory.GetFiles(_recordingsDirectory, $"{id}.*")
                .FirstOrDefault(f => !f.EndsWith(".txt") && !f.EndsWith(".json"));

            if (audioFile == null || !System.IO.File.Exists(audioFile))
                return NotFound($"Recording with ID '{id}' was not found in storage.");

            _logger.LogInformation("Starting on-demand transcription for recording {Id} ({File})", id, audioFile);

            try
            {
                await using var readStream = System.IO.File.OpenRead(audioFile);
                var transcriptResult = await _whisperClient.TranscribeAudioAsync(
                    readStream,
                    Path.GetFileName(audioFile),
                    language,
                    model,
                    ct
                );

                // Save output text file
                var textFileName = $"{id}.txt";
                var textFilePath = Path.Combine(_recordingsDirectory, textFileName);
                await System.IO.File.WriteAllTextAsync(textFilePath, transcriptResult.Text, ct);

                // Update database entity if present
                if (int.TryParse(id, out var intId))
                {
                    var recording = await _dbContext.Recordings.FirstOrDefaultAsync(r => r.Id == intId, ct);
                    if (recording != null)
                    {
                        recording.NotePath = textFilePath;
                        recording.ProcessingResults.Add(new ProcessingResult
                        {
                            Step = ProcessingStep.Transcription,
                            Message = $"Completed ({transcriptResult.Language}, {transcriptResult.AudioSeconds}s audio)",
                            CreatedAt = DateTimeOffset.UtcNow
                        });
                        await _dbContext.SaveChangesAsync(ct);
                    }
                }

                return Ok(new
                {
                    id,
                    transcriptPath = textFilePath,
                    transcript = transcriptResult.Text,
                    details = transcriptResult
                });
            }
            catch (WhisperException ex)
            {
                _logger.LogError(ex, "Whisper transcription failed for {Id}: {Message}", id, ex.Message);

                if (int.TryParse(id, out var intId))
                {
                    var recording = await _dbContext.Recordings.FirstOrDefaultAsync(r => r.Id == intId, ct);
                    if (recording != null)
                    {
                        recording.ProcessingResults.Add(new ProcessingResult
                        {
                            Step = ProcessingStep.Transcription,
                            Message = $"Failed: {ex.Message}",
                            CreatedAt = DateTimeOffset.UtcNow
                        });
                        await _dbContext.SaveChangesAsync(ct);
                    }
                }

                return StatusCode(ex.StatusCode ?? StatusCodes.Status502BadGateway, new
                {
                    id,
                    error = ex.Message
                });
            }
        }

        [HttpGet("{id}/transcript")]
        public async Task<IActionResult> GetTranscript(string id, CancellationToken ct = default)
        {
            var textFilePath = Path.Combine(_recordingsDirectory, $"{id}.txt");
            if (!System.IO.File.Exists(textFilePath))
                return NotFound($"Transcript for recording '{id}' not found.");

            var text = await System.IO.File.ReadAllTextAsync(textFilePath, ct);
            return Ok(new
            {
                id,
                text,
                filePath = textFilePath
            });
        }

        [HttpGet("{id}/transcript/file")]
        public IActionResult DownloadTranscriptFile(string id)
        {
            var textFilePath = Path.Combine(_recordingsDirectory, $"{id}.txt");
            if (!System.IO.File.Exists(textFilePath))
                return NotFound($"Transcript text file for recording '{id}' not found.");

            return PhysicalFile(textFilePath, "text/plain; charset=utf-8", $"{id}.txt");
        }
    }
}
