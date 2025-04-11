using GestionIntervention.Models.Dtos;
using GestionIntervention.Models.Entities;
using GestionIntervention.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionIntervention.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeInterventionController : ControllerBase
    {
        private readonly IService<TypeIntervention> _typeInterventionService;

        public TypeInterventionController(IService<TypeIntervention> typeInterventionService)
        {
            _typeInterventionService = typeInterventionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TypeIntervention>>> GetAll()
        {
            var typeInterventions = await _typeInterventionService.GetAllAsync();
            var typeInterventionDtos = typeInterventions.Select(typeIntervention => new TypeInterventionDto
            {
                Id = typeIntervention.Id,
                Nom = typeIntervention.Nom,
            });
            return Ok(typeInterventionDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(int id)
        {
            var typeIntervention = await _typeInterventionService.GetByIdAsync(id);
            if (typeIntervention == null)
            {
                return NotFound();
            }
            return Ok(new TypeInterventionDto{ Id = typeIntervention.Id, Nom = typeIntervention.Nom});
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] string newTypeIntervention)
        {
            var typeIntervention = new TypeIntervention
            {
                Nom = newTypeIntervention
            };


            await _typeInterventionService.CreateAsync(typeIntervention);
            var typeInterventionResponse = new TypeInterventionDto
            {
                Id = typeIntervention.Id,
                Nom = newTypeIntervention,
            };
            return CreatedAtAction(nameof(GetById), new { id = typeIntervention.Id }, typeInterventionResponse);
        }
    }
}
