using Microsoft.AspNetCore.Identity;

namespace GestionIntervention.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireTime { get; set; }

        public string DisplayName { get; set; }
    }
}
