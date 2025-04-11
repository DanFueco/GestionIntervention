using GestionIntervention.Models.Entities;
using GestionIntervention.Repositories.Interfaces;
using GestionIntervention.Services.Interfaces;

namespace GestionIntervention.Services
{
    public class ClientService : IService<Client>
    {
        private readonly IDataAccess<Client> _dataAccess;

        public ClientService (IDataAccess<Client> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<List<Client>> GetAllAsync() =>
            _dataAccess.GetAllAsync();

        public Task<Client?> GetByIdAsync(int id) =>
            _dataAccess.GetById(id);

        public Task CreateAsync(Client client) => 
            _dataAccess.AddAsync(client);

        public Task UpdateAsync(Client client) =>
            _dataAccess.UpdateAsync(client);

        public Task DeleteAsync(int id) =>
            _dataAccess.DeleteAsync(id);
    }
}
