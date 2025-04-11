namespace GestionIntervention.Models.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string DisplayName { get; set; }
    }
}
