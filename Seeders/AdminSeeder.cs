using GestionIntervention.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace GestionIntervention.Seeders
{
    public class AdminSeeder : ISeeder
    {
        private readonly IServiceProvider _serviceProvider;

        public AdminSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SeedAsync(AppDbContext context)
        {
            using var scope = _serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var adminEmail = "admin@local";
            var adminPassword = "Admin@12";

            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "Admin",
                    DisplayName = "Admin",
                    Email = adminEmail,
                };
                await userManager.CreateAsync(user, adminPassword);
                await userManager.AddToRoleAsync(user, "admin");
            }
        }

    }
}
