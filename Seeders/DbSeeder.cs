namespace GestionIntervention.Seeders
{
    public class DbSeeder
    {
        private readonly IEnumerable<ISeeder> _seeders;

        public DbSeeder(IEnumerable<ISeeder> seeders)
        {
            _seeders = seeders;
        }

        public async Task SeedAsync(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            foreach (var seeder in _seeders)
            {
                await seeder.SeedAsync(context);
            }
        }
    }
}
