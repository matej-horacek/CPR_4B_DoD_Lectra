using System.Net.Http.Headers;
using System.Text.Json;

namespace api.Services;

public class WhisperClient : IWhisperClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WhisperClient> _logger;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public WhisperClient(HttpClient httpClient, ILogger<WhisperClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<WhisperTranscriptionResponse> TranscribeAudioAsync(
        Stream audioStream,
        string fileName,
        string? language = null,
        string? model = null,
        CancellationToken ct = default)
    {
        using var form = CreateMultipartForm(audioStream, fileName, language, model, format: "json");

        _logger.LogInformation("Sending audio file {FileName} to Whisper service at {BaseAddress}transcribe...",
            fileName, _httpClient.BaseAddress);

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.PostAsync("transcribe", form, ct);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to connect to Whisper service at {BaseAddress}", _httpClient.BaseAddress);
            throw new WhisperException($"Unable to contact Whisper service: {ex.Message}", innerException: ex);
        }

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(ct);
            _logger.LogError("Whisper service returned error {StatusCode}: {Error}", response.StatusCode, errorContent);
            throw new WhisperException($"Whisper service error ({(int)response.StatusCode}): {errorContent}", (int)response.StatusCode);
        }

        var jsonStream = await response.Content.ReadAsStreamAsync(ct);
        var result = await JsonSerializer.DeserializeAsync<WhisperTranscriptionResponse>(jsonStream, JsonOptions, ct);

        if (result == null)
        {
            throw new WhisperException("Received empty response from Whisper service.");
        }

        _logger.LogInformation("Whisper transcription completed for {FileName} ({Language}, {Seconds}s audio in {Elapsed}s)",
            fileName, result.Language, result.AudioSeconds, result.ElapsedSeconds);

        return result;
    }

    public async Task<string> TranscribeAudioAsTextAsync(
        Stream audioStream,
        string fileName,
        string? language = null,
        string? model = null,
        CancellationToken ct = default)
    {
        using var form = CreateMultipartForm(audioStream, fileName, language, model, format: "txt");

        _logger.LogInformation("Requesting plain text transcription for {FileName} from Whisper service...", fileName);

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.PostAsync("transcribe", form, ct);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to connect to Whisper service at {BaseAddress}", _httpClient.BaseAddress);
            throw new WhisperException($"Unable to contact Whisper service: {ex.Message}", innerException: ex);
        }

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(ct);
            _logger.LogError("Whisper service returned error {StatusCode}: {Error}", response.StatusCode, errorContent);
            throw new WhisperException($"Whisper service error ({(int)response.StatusCode}): {errorContent}", (int)response.StatusCode);
        }

        return await response.Content.ReadAsStringAsync(ct);
    }

    private static MultipartFormDataContent CreateMultipartForm(
        Stream audioStream,
        string fileName,
        string? language,
        string? model,
        string format)
    {
        var form = new MultipartFormDataContent();

        var streamContent = new StreamContent(audioStream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        form.Add(streamContent, "file", fileName);

        form.Add(new StringContent(format), "format");

        if (!string.IsNullOrWhiteSpace(model))
        {
            form.Add(new StringContent(model), "model");
        }

        if (!string.IsNullOrWhiteSpace(language))
        {
            form.Add(new StringContent(language), "language");
        }

        return form;
    }
}
