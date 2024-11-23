using QRMenu.Core.Entities;
using QRMenu.Core.Enums;
using QRMenu.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(ApplicationDbContext context)
    {
        if (!context.Users.Any())
        {
            var defaultUser = new User
            {
                Email = "admin@demo.com",
                PasswordHash = "1234",
                FullName = "Admin User",
                Role = UserRole.Admin,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            context.Users.Add(defaultUser);
            await context.SaveChangesAsync();
        }
    }
}