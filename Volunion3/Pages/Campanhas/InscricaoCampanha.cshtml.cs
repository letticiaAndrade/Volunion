using Volunion3.Models;
using Volunion3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Volunion3.Pages.Campanhas
{
    public class InscricaoCampanhaModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InscricaoCampanhaModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Campanha Campanha { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Campanha = await _context.Campanhas
            .Include(c => c.CampanhaVoluntarios)
            .FirstOrDefaultAsync(c => c.Id == id);


            if (Campanha == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostInscreverAsync(int campanhaId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var campanha = await _context.Campanhas
                .Include(c => c.CampanhaVoluntarios)  
                .FirstOrDefaultAsync(c => c.Id == campanhaId);

            if (campanha == null) return NotFound();

            bool jaInscrito = await _context.CampanhaVoluntarios
                .AnyAsync(cv => cv.CampanhaId == campanhaId && cv.VoluntarioId == user.Id);

            if (jaInscrito) return BadRequest("Você já está inscrito nesta campanha.");

            var inscricao = new CampanhaVoluntario
            {
                CampanhaId = campanhaId,
                VoluntarioId = user.Id
            };

            _context.CampanhaVoluntarios.Add(inscricao);
            await _context.SaveChangesAsync();

            _context.Entry(campanha).Reload();

            return RedirectToPage("MinhasInscricoes");
        }

    }
}
