using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class RecordingService(AppDbContext db)
{
    public async Task<IEnumerable<RecordingDTO>> GetRecordingsAsync(CancellationToken ct)
    {
        var recordings = await db.Recordings.ToListAsync(ct);
        
        return recordings.Select(r => r.Dto());
    }
}