using Microsoft.AspNetCore.Identity;

namespace GestionIntervention.Models.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
