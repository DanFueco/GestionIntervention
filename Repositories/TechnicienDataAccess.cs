using GestionIntervention.Models.Entities;
using GestionIntervention.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionIntervention.Repositories
{
    public class TechnicienDataAccess
    {
        private readonly AppDbContext _context;

        public TechnicienDataAccess(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Technicien>> GetAllAsync() =>
            await _context.Techniciens.ToListAsync();

        public async Task<Technicien?> GetById(string id) =>
            await _context.Techniciens.FindAsync(id);


        public async Task DeleteAsync(string id)
        {
            var technicien = await _context.Techniciens.FindAsync(id);
            if (technicien != null)
            {
                _context.Techniciens.Remove(technicien);
                await _context.SaveChangesAsync();
            }
        }
    }
}
