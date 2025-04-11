using GestionIntervention.Models.Entities;
using GestionIntervention.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionIntervention.Repositories
{
    public class ClientDataAccess : IDataAccess<Client>
    {
        private readonly AppDbContext _context;

        public ClientDataAccess(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllAsync() =>
            await _context.Clients.ToListAsync();

        public async Task<Client?> GetById(int id) =>
            await _context.Clients.FindAsync(id);

        public async Task AddAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}
