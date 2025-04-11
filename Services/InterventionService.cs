using GestionIntervention.Models.Dtos;
using GestionIntervention.Models.Entities;
using GestionIntervention.Repositories;
using GestionIntervention.Repositories.Interfaces;
using GestionIntervention.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionIntervention.Services
{
    public class InterventionService : IService<Intervention>
    {
        private readonly IService<Client> _clientService;
        private readonly IService<TypeIntervention> _typeInterventionService;
        private readonly TechnicienService _technicienService;
        private readonly InterventionDataAccess _dataAccess;

        public InterventionService (
            InterventionDataAccess dataAccess,
            IService<Client> clientService,
            IService<TypeIntervention> typeInterventionService,
            TechnicienService technicienService
            )
        {
            _dataAccess = dataAccess;
            _clientService = clientService;
            _typeInterventionService = typeInterventionService;
            _technicienService = technicienService;
        }

        public Task<List<Intervention>> GetAllAsync() =>
            _dataAccess.GetAllAsync();

        public Task<Intervention?> GetByIdAsync(int id) =>
            _dataAccess.GetById(id);

        public Task CreateAsync(Intervention intervention) => 
            _dataAccess.AddAsync(intervention);

        public Task UpdateAsync(Intervention intervention) =>
            _dataAccess.UpdateAsync(intervention);

        public Task DeleteAsync(int id) =>
            _dataAccess.DeleteAsync(id);
        public async Task<Intervention> CreateIntervention(CreateInterventionDto newinterventionDto)
        {
            var client = await _clientService.GetByIdAsync(newinterventionDto.ClientId);
            var typeIntervention = await _typeInterventionService.GetByIdAsync(newinterventionDto.TypeInterventionId);
            if (client == null || typeIntervention == null)
                throw new Exception("Client ou type de service introuvable");
            var intervention = new Intervention
            {
                Client = client,
                Type = typeIntervention,
                Date = newinterventionDto.Date,
            };

            intervention.Techniciens = new List<Technicien>();

            foreach (var techId in newinterventionDto.TechniciensId)
            {
                var technicien = await _technicienService.GetTechnicienByIdAsync(techId);
                if (technicien == null)
                    throw new Exception($"Technicien {techId} introuvable");

                intervention.Techniciens.Add(technicien);
            }

            await CreateAsync(intervention);

            return intervention;
        }

        public async Task<List<Intervention>> GetByTechnicienIdAsync(string technicienId)
        {
            return await _dataAccess.GetByTechnicienId(technicienId);
        }
    }
}
