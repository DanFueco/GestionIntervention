using GestionIntervention.Models.Entities;
using System.Security.Claims;

namespace GestionIntervention.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtTokenAsync(ApplicationUser user);

        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredTokens(string token);
    }
}
