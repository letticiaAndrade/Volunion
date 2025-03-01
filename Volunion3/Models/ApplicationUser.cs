using Microsoft.AspNetCore.Identity;

namespace Volunion3.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; } = "";

        public string? Descricao { get; set; } = "";
        public string? Localizacao { get; set; } = "";
        public string TipoUsuario { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    }
}
