using api.Data;
using api.Data.Entities;
using api.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace api.Services;

public class RecordingService(AppDbContext db)
{
    public async Task<IEnumerable<RecordingDTO>> GetRecordingsAsync(CancellationToken ct)
    {
        var recordings = await db.Recordings.ToListAsync(ct);

        return recordings.Select(r => r.Dto());
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
