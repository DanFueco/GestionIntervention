using GestionIntervention.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionIntervention
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {

        public DbSet<Technicien> Techniciens { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<TypeIntervention> TypeInterventions { get; set; }
        public DbSet<Intervention> Interventions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Technicien>().ToTable("Techniciens");

            modelBuilder.Entity<Intervention>()
                .HasMany(i => i.Techniciens)
                .WithMany(u => u.Interventions)
                .UsingEntity(j => j.ToTable("InterventionTechnicien"));
        }
    }
}
