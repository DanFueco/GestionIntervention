using GestionIntervention.Models.Entities;
using GestionIntervention.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionIntervention.Repositories
{
    public class TypeInterventionDataAccess : IDataAccess<TypeIntervention>
    {
        private readonly AppDbContext _context;

        public TypeInterventionDataAccess(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TypeIntervention>> GetAllAsync() =>
            await _context.TypeInterventions.ToListAsync();

        public async Task<TypeIntervention?> GetById(int id) =>
            await _context.TypeInterventions.FindAsync(id);

        public async Task AddAsync(TypeIntervention typeIntervention)
        {
            _context.TypeInterventions.Add(typeIntervention);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TypeIntervention typeIntervention)
        {
            _context.TypeInterventions.Update(typeIntervention);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var typeIntervention = await _context.TypeInterventions.FindAsync(id);
            if (typeIntervention != null)
            {
                _context.TypeInterventions.Remove(typeIntervention);
                await _context.SaveChangesAsync();
            }
        }
    }
}
