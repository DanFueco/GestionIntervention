namespace GestionIntervention.Models.Dtos
{
    public class InterventionDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string TypeIntervention { get; set; }

        public string Client { get; set; }

        public List<string> Techniciens { get; set; }
    }
}
