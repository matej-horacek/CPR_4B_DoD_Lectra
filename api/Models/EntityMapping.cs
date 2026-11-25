using api.Data.Entities;

namespace api.Models;

public static class EntityMapping
{
    public static RecordingDTO Dto(this Recording recording)
    {
        return new RecordingDTO(
            recording.Id,
            recording.UserId,
            recording.SubjectId,
            recording.Title,
            recording.FilePath,
            recording.NotePath,
            recording.CreatedAt
        );
    }
}