using GestionIntervention.Models.Dtos;
using GestionIntervention.Models.DTOs;
using GestionIntervention.Models.Entities;
using GestionIntervention.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionIntervention.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechnicienController : ControllerBase
    {
        private readonly TechnicienService _technicienService;

        public TechnicienController(TechnicienService technicienService)
        {
            _technicienService= technicienService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTechnicien([FromBody] RegisterDto newTechnicien)
        {
            var result = await _technicienService.CreateTechnicienAsync(newTechnicien);

            if (result.Succeeded)
            {
                return Ok(new { message = "Technicien créé avec succès" });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Intervention>>> GetTechnicianById(string id)
        {
            var technicien = await _technicienService.GetTechnicienByIdAsync(id);
            if (technicien == null)
            {
                return NotFound();
            }
            var technicienDto = new TechnicienDto
            {
                Id = technicien.Id,
                Name = technicien.DisplayName,
                Email = technicien.Email
            };
            return Ok(technicienDto);
        }

        [HttpGet]
        public async Task<ActionResult<List<Technicien>>> GetAll()
        {
            var techniciens = await _technicienService.GetTechniciensAsync();
            var techniciensDtos = techniciens.Select(technicien => new TechnicienDto
            {
                Id = technicien.Id,
                Name = technicien.DisplayName,
                Email = technicien.Email
            });
            return Ok(techniciensDtos);
        }
    }
}
