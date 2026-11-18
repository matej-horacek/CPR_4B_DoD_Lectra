using api.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityUserContext<User, int>(options)
{
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Recording> Recordings => Set<Recording>();
    public DbSet<ProcessingResult> ProcessingResults => Set<ProcessingResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
