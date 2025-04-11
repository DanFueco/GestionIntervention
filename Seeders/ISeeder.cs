namespace GestionIntervention.Seeders
{
    public interface ISeeder
    {
        Task SeedAsync(AppDbContext context);
    }
}
