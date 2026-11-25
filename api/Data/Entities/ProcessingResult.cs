namespace api.Data.Entities;

public class ProcessingResult
{
    public int Id { get; set; }
    public int RecordingId { get; set; }
    public ProcessingStep Step { get; set; }
    public string Message { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Recording Recording { get; set; } = null!;
}
