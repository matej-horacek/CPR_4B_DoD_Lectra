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
}