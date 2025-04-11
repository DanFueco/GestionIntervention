using GestionIntervention.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace GestionIntervention.Seeders
{
    public class RoleSeeder : ISeeder
    {
        private readonly IServiceProvider _serviceProvider;

        public RoleSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SeedAsync(AppDbContext context)
        {
            using var scope = _serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var roles = new[] { "admin", "technicien", "client" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role});
                }
            }
        }
    }
}
