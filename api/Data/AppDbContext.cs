using api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ExampleItem> ExampleItems => Set<ExampleItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Načte všechny IEntityTypeConfiguration z Data/Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
