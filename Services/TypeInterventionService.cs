using GestionIntervention.Models.Entities;
using GestionIntervention.Repositories.Interfaces;
using GestionIntervention.Services.Interfaces;

namespace GestionIntervention.Services
{
    public class TypeInterventionService : IService<TypeIntervention>
    {
        private readonly IDataAccess<TypeIntervention> _dataAccess;

        public TypeInterventionService (IDataAccess<TypeIntervention> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<List<TypeIntervention>> GetAllAsync() =>
            _dataAccess.GetAllAsync();

        public Task<TypeIntervention?> GetByIdAsync(int id) =>
            _dataAccess.GetById(id);

        public Task CreateAsync(TypeIntervention typeIntervention) => 
            _dataAccess.AddAsync(typeIntervention);

        public Task UpdateAsync(TypeIntervention typeIntervention) =>
            _dataAccess.UpdateAsync(typeIntervention);

        public Task DeleteAsync(int id) =>
            _dataAccess.DeleteAsync(id);
    }
}
