using System.ComponentModel.DataAnnotations;

namespace Volunion3.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }

        [Required]
        public int CampanhaId { get; set; }
        public Campanha Campanha { get; set; }

        [Required]
        public string VoluntarioId { get; set; }
        public ApplicationUser Voluntario { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "A nota deve estar entre 0 e 5.")]
        public int Nota { get; set; }

        public string Comentario { get; set; }
        public DateTime DataAvaliacao { get; set; } = DateTime.UtcNow;
    }
}
