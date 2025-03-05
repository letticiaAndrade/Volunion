using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Volunion3.Models
{
    public class Campanha
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Local { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFim { get; set; }

        [Required]
        public int NumeroMaximoVoluntarios { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public string OrganizacaoId { get; set; }

        public ApplicationUser Organizacao { get; set; }

        public List<CampanhaVoluntario> CampanhaVoluntarios { get; set; } = new();
    }
}
