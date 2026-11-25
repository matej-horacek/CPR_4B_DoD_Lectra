using System.Text.Json.Serialization;

namespace api.Services;

public record WhisperSegment(
    [property: JsonPropertyName("start")] double Start,
    [property: JsonPropertyName("end")] double End,
    [property: JsonPropertyName("text")] string Text
);

public record WhisperTranscriptionResponse(
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("language")] string Language,
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("audio_seconds")] double AudioSeconds,
    [property: JsonPropertyName("elapsed_seconds")] double ElapsedSeconds,
    [property: JsonPropertyName("segments")] List<WhisperSegment>? Segments
);

public class WhisperException : Exception
{
    public int? StatusCode { get; }

    public WhisperException(string message, int? statusCode = null, Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}

public interface IWhisperClient
{
    Task<WhisperTranscriptionResponse> TranscribeAudioAsync(
        Stream audioStream,
        string fileName,
        string? language = null,
        string? model = null,
        CancellationToken ct = default
    );

    Task<string> TranscribeAudioAsTextAsync(
        Stream audioStream,
        string fileName,
        string? language = null,
        string? model = null,
        CancellationToken ct = default
    );
}
