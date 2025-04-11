namespace GestionIntervention.Models.Entities
{
    public class Technicien : ApplicationUser
    {
        public ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();
    }
}
