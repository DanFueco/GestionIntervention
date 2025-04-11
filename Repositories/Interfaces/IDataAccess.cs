using GestionIntervention.Models.Entities;

namespace GestionIntervention.Repositories.Interfaces
{
    public interface IDataAccess<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetById(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
