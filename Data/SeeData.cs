using Microsoft.AspNetCore.Identity;
using UserManager.Models;

namespace UserManager.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Users.Any(u => u.Role == UserRole.Admin))
            {
                return; // Admin user already exists
            }

            var passwordHasher = new PasswordHasher<User>();
            var admin = new User
            {
                Id = Guid.NewGuid(),
                Email = "admin@example.com",
                PasswordResetToken="",
                Role = UserRole.Admin
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin@123");

            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}
