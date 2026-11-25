using api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.Role).HasConversion<string>().HasMaxLength(50);
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
    }
}
