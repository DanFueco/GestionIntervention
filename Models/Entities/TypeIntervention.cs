namespace GestionIntervention.Models.Entities
{
    public class TypeIntervention
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public ICollection<Intervention> Interventions { get; set; }
    }
}
