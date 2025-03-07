using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Volunion3.Models;
using Volunion3.Services;

namespace Volunion3.Pages.Campanhas
{
    public class AvaliacoesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AvaliacoesModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Campanha Campanha { get; set; }
        public List<Avaliacao> Avaliacoes { get; set; }

        [BindProperty]
        public int Nota { get; set; }

        [BindProperty]
        public string Comentario { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Campanha = await _context.Campanhas
                .Include(c => c.Avaliacoes)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Campanha == null)
                return NotFound();

            Avaliacoes = await _context.Avaliacoes
                .Where(a => a.CampanhaId == id)
                .Include(a => a.Voluntario)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var campanha = await _context.Campanhas
                .Include(c => c.CampanhaVoluntarios)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (campanha == null || !campanha.CampanhaVoluntarios.Any(cv => cv.VoluntarioId == user.Id))
            {
                TempData["Erro"] = "You can only rate campaigns you participated in..";
                return RedirectToPage(new { id });
            }

            if (DateTime.UtcNow <= campanha.DataFim)
            {
                TempData["Erro"] = "You can only evaluate after the campaign ends.";
                return RedirectToPage(new { id });
            }

            var avaliacao = new Avaliacao
            {
                CampanhaId = id,
                VoluntarioId = user.Id,
                Nota = Nota,
                Comentario = Comentario,
                DataAvaliacao = DateTime.UtcNow
            };

            _context.Avaliacoes.Add(avaliacao);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Evaluation registered successfully!";
            return RedirectToPage(new { id });
        }
    }
}
