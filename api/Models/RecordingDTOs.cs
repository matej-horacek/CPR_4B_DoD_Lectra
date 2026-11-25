namespace api.Models;

public record RecordingDTO
(
    int Id,
    int? UserId,
    int SubjectId,
    string Title,
    string FilePath,
    string? NotePath,
    DateTimeOffset CreatedAt
);