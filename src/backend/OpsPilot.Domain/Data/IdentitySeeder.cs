using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OpsPilot.Domain.Entities.Identity;

namespace OpsPilot.Domain.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            string[] roles =
            [
                "Admin",
                "Developer",
                "Viewer"
            ];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new Role
                        {
                            Name = role
                        });
                }
            }
        }
    }
}
