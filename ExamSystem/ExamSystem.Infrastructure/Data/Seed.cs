using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ExamSystem.Infrastructure.Data
{
    public class Seed
    {
        public static async Task SeedRole(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var admins = new[]
            {
                new { UserName = "admin1", Email = "admin1@example.com", Password = "Admin@123" },
                new { UserName = "admin2", Email = "admin2@example.com", Password = "Admin@123" },
                new { UserName = "admin3", Email = "admin3@example.com", Password = "Admin@123" }
            };

            foreach (var admin in admins)
            {
                var existingUser = await userManager.FindByEmailAsync(admin.Email);
                if (existingUser == null)
                {
                    var newAdmin = new IdentityUser
                    {
                        UserName = admin.UserName,
                        Email = admin.Email
                    };

                    var result = await userManager.CreateAsync(newAdmin, admin.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newAdmin, "Admin");
                    }
                }
            }
        }
    }

}
