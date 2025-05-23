﻿using GestionIntervention.Models.Dtos;
using GestionIntervention.Models.Entities;
using GestionIntervention.Repositories.Interfaces;
using GestionIntervention.Services;
using GestionIntervention.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace GestionIntervention.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterventionController : ControllerBase
    {
        private readonly InterventionService _interventionService;

        public InterventionController(InterventionService interventionService)
        {
            _interventionService = interventionService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Intervention>>> GetAll()
        {
            var interventions = await _interventionService.GetAllAsync();
            //var interventionsDtos = interventions.Select(intervention => new InterventionDto
            //{
            //    Id = intervention.Id,
            //    Date = intervention.Date,
            //    TypeIntervention = intervention.Type.Nom,
            //    Client = intervention.Client.Name,
            //    Techniciens = intervention.Techniciens.Select(technicien => technicien.DisplayName).ToList(),
            //});
            return Ok(interventions);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateInterventionDto newInterventionDto)
        {
            try
            {
                var intervention = await _interventionService.CreateIntervention(newInterventionDto);
                var interventionDto = new InterventionDto
                {
                    Id = intervention.Id,
                    Date = intervention.Date,
                    TypeIntervention = intervention.Type.Nom,
                    Client = intervention.Client.Name,
                    Techniciens = intervention.Techniciens.Select(technicien => technicien.DisplayName).ToList(),
                };
                return Ok(interventionDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new {error = ex .Message});
            }
        }

        [HttpGet("mine")]
        [Authorize(Roles = "technicien")]
        public async Task<ActionResult<List<Intervention>>> GetMine()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var interventions = await _interventionService.GetByTechnicienIdAsync(userId);
            var interventionsDtos = interventions.Select(intervention => new InterventionDto
            {
                Id = intervention.Id,
                Date = intervention.Date,
                TypeIntervention = intervention.Type.Nom,
                Client = intervention.Client.Name,
                Techniciens = intervention.Techniciens.Select(technicien => technicien.DisplayName).ToList(),
            });
            return Ok(interventionsDtos);
        }
    }
}
