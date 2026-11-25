using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class RecordingController : Controller
    {
        [HttpPost]
        [Route("{controller}/upload")]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken ct)
        {
            var id = Guid.CreateVersion7();

            var directory = Path.Combine(Path.GetTempPath(), "recordings");

            Directory.CreateDirectory(directory);

            var filePath = Path.Combine(directory, $"{id}.mp3");

            try
            {
                await using var destination = new FileStream(
                    filePath,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 81920,
                    useAsync: true
                );

                await file.CopyToAsync(destination, ct);

                return Ok(new { id, size = destination.Length });
            }
            catch
            {
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                throw;
            }
        }
    }
}
