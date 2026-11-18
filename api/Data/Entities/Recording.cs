namespace api.Data.Entities;

public class Recording
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public int SubjectId { get; set; }
    public string Title { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public string? NotePath { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public User? User { get; set; }
    public Subject Subject { get; set; } = null!;
    public ICollection<ProcessingResult> ProcessingResults { get; set; } = [];
}
