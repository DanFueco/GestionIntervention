using GestionIntervention.Models.Entities;
using GestionIntervention.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionIntervention.Repositories
{
    public class InterventionDataAccess : IDataAccess<Intervention>
    {
        private readonly AppDbContext _context;

        public InterventionDataAccess(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Intervention>> GetAllAsync() =>
            await _context.Interventions.ToListAsync();

        public async Task<Intervention?> GetById(int id) =>
            await _context.Interventions.FindAsync(id);

        public async Task AddAsync(Intervention intervention)
        {
            _context.Interventions.Add(intervention);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Intervention intervention)
        {
            _context.Interventions.Update(intervention);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention != null)
            {
                _context.Interventions.Remove(intervention);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Intervention>> GetByTechnicienId(string id)
        {
            return await _context.Interventions
                .Where(i => i.Techniciens.Any(t => t.Id == id))
                .ToListAsync();
        }
    }
}
