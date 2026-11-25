using api.Data;
using api.Data.Entities;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class SubjectService(AppDbContext db)
{
    public async Task<IEnumerable<Subject>> GetSubjectsAsync(CancellationToken ct)
    {
        var subjects = await db.Subjects.ToListAsync(ct);

        return subjects;
    }
    
    public async Task<Subject> GetSubjectByIdAsync(int id, CancellationToken ct)
    {
        var subject = await db.Subjects.Where(s => s.Id == id).FirstOrDefaultAsync(ct);
        
        if (subject is null)
            throw new Exception($"Subject with id {id} not found");

        return subject;
    }
    
    public async Task<Subject> CreateSubjectAsync(string name, CancellationToken ct)
    {
        var subject = new Subject
        {
            Name = name
        };

        db.Subjects.Add(subject);
        await db.SaveChangesAsync(ct);

        return subject;
    }
    
    public async Task<Subject> RenameSubjectAsync(int id, string newName, CancellationToken ct)
    {
        var subject = await GetSubjectByIdAsync(id, ct);
        
        subject.Name = newName;
        await db.SaveChangesAsync(ct);

        return subject;
    }
}