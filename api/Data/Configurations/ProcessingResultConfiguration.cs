using api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations;

public class ProcessingResultConfiguration : IEntityTypeConfiguration<ProcessingResult>
{
    public void Configure(EntityTypeBuilder<ProcessingResult> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Step).HasConversion<string>().HasMaxLength(50);
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

        builder.HasOne(e => e.Recording)
            .WithMany(r => r.ProcessingResults)
            .HasForeignKey(e => e.RecordingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
