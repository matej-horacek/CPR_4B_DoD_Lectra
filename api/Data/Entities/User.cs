using Microsoft.AspNetCore.Identity;

namespace api.Data.Entities;

public class User : IdentityUser<int>
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public UserRole Role { get; set; } = UserRole.User;
}