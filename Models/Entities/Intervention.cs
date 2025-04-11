namespace GestionIntervention.Models.Entities
{
    public class Intervention
    {
        public int Id { get; set; } = default;

        public DateTime Date { get; set; }

        public TypeIntervention Type { get; set; }

        public Client Client { get; set; }

        public ICollection<Technicien> Techniciens { get; set; }

    }
}
