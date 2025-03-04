using Volunion3.Models;

public class CampanhaVoluntario
{
    public int CampanhaId { get; set; }
    public Campanha Campanha { get; set; }

    public string VoluntarioId { get; set; }
    public ApplicationUser Voluntario { get; set; }

    public DateTime InscricaoData { get; set; } = DateTime.Now;
}
