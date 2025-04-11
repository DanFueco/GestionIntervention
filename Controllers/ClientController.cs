using GestionIntervention.Models.Dtos;
using GestionIntervention.Models.Entities;
using GestionIntervention.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionIntervention.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IService<Client> _clientService;

        public ClientController(IService<Client> clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAll()
        {
            var clients = await _clientService.GetAllAsync();
            var clientsDtos = clients.Select(client => new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
            });
            return Ok(clientsDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(int id)
        {
            var client = await _clientService.GetByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(new ClientDto{ Id = client.Id, Name = client.Name});
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] string nameClient)
        {
            var client = new Client
            {
                Name = nameClient
            };


            await _clientService.CreateAsync(client);
            var clientResponse = new ClientDto
            {
                Id = client.Id,
                Name = nameClient
            };
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, clientResponse);
        }
    }
}
