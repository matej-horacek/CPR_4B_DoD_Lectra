using api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations;

public class ExampleItemConfiguration : IEntityTypeConfiguration<ExampleItem>
{
    public void Configure(EntityTypeBuilder<ExampleItem> builder)
    {
        builder.ToTable("example_items");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Description).HasMaxLength(2000);
    }
}
