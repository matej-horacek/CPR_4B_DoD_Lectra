using api.Data;
using api.Data.Entities;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class RecordingService(AppDbContext db)
{
    public async Task<IEnumerable<Recording>> GetRecordingsAsync(CancellationToken ct)
    {
        return await db.Recordings.ToListAsync(ct);
    }

    public async Task<RecordingDTO> SaveRecordingAsync(
        string filePath,
        int userId,
        int subjectId,
        string title,
        CancellationToken ct
    )
    {
        var entity = new Recording
        {
            UserId = userId,
            SubjectId = subjectId,
            Title = title,
            FilePath = filePath,
        };

        db.Recordings.Add(entity);
        await db.SaveChangesAsync(ct);

        return entity.Dto();
    }
}