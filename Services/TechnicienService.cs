using GestionIntervention.Models.DTOs;
using GestionIntervention.Models.Entities;
using GestionIntervention.Repositories.Interfaces;
using GestionIntervention.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GestionIntervention.Services
{
    public class TechnicienService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public TechnicienService(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityResult> CreateTechnicienAsync(RegisterDto newTechnicien)
        {
            var technicien = new Technicien
            {
                UserName = newTechnicien.DisplayName,
                Email = newTechnicien.Email,
                DisplayName = newTechnicien.DisplayName,
            };

            var result = await _userManager.CreateAsync(technicien, newTechnicien.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(technicien, "technicien");
            }

            return result;
        }

        public async Task<Technicien> GetTechnicienByIdAsync(string id)
        {
            var technicien = await _userManager.FindByIdAsync(id) as Technicien;
            return technicien;
        }
        
        public async Task<List<Technicien>> GetTechniciensAsync()
        {
            var techniciens = await _context.Techniciens.ToListAsync();
            return techniciens;
        }
    }
}
