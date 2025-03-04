using Volunion3.Models;
using Volunion3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Volunion3.Pages.Campanhas
{
    public class TodasCampanhasModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodasCampanhasModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Campanha> Campanhas { get; set; }
        public bool UsuarioEhVoluntario { get; set; }

        public async Task OnGetAsync()
        {
            Campanhas = await _context.Campanhas
            .Include(c => c.CampanhaVoluntarios)  
            .Include(c => c.Organizacao)       
            .ToListAsync();

            var user = await _userManager.GetUserAsync(User);
            UsuarioEhVoluntario = user != null && await _userManager.IsInRoleAsync(user, "voluntario");
        }

        public async Task<IActionResult> OnPostInscreverAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || !await _userManager.IsInRoleAsync(user, "voluntario"))
            {
                return Unauthorized();
            }

            var campanha = await _context.Campanhas
                .Include(c => c.CampanhaVoluntarios)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (campanha == null || campanha.CampanhaVoluntarios.Any(cv => cv.VoluntarioId == user.Id))
            {
                return BadRequest("Você já está inscrito nesta campanha ou a campanha não existe.");
            }

            if (campanha.CampanhaVoluntarios.Count >= campanha.NumeroMaximoVoluntarios)
            {
                return BadRequest("A campanha já atingiu o número máximo de voluntários.");
            }

            var inscricao = new CampanhaVoluntario
            {
                CampanhaId = campanha.Id,
                VoluntarioId = user.Id
            };

            _context.CampanhaVoluntarios.Add(inscricao);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
