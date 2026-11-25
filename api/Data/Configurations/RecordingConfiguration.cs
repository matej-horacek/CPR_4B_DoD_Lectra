using api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations;

public class RecordingConfiguration : IEntityTypeConfiguration<Recording>
{
    public void Configure(EntityTypeBuilder<Recording> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
        builder.HasIndex(e => e.FilePath).IsUnique();
        builder.HasIndex(e => e.NotePath).IsUnique();

        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Subject)
            .WithMany(s => s.Recordings)
            .HasForeignKey(e => e.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
