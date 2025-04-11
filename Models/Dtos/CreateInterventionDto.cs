namespace GestionIntervention.Models.Dtos
{
    public class CreateInterventionDto
    { 
        public DateTime Date { get; set; }
        public int TypeInterventionId { get; set; }

        public int ClientId { get; set; }

        public List<string> TechniciensId { get; set; }
    }
}
