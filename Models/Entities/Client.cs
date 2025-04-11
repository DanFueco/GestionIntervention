namespace GestionIntervention.Models.Entities
{
    public class Client
    {
        public int Id { get; set; } = default;
        public string Name { get; set; }
        public ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();
    }
}
