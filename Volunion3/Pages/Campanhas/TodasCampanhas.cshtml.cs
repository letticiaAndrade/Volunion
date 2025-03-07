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

        public string Titulo { get; set; }
        public string Local { get; set; }
        public DateTime? DataInicio { get; set; }

        public async Task OnGetAsync(string titulo, string local, DateTime? dataInicio)
        {
            var campanhasQuery = _context.Campanhas.AsQueryable();

            if (!string.IsNullOrEmpty(titulo))
            {
                campanhasQuery = campanhasQuery.Where(c => c.Titulo.Contains(titulo));
            }

            if (!string.IsNullOrEmpty(local))
            {
                campanhasQuery = campanhasQuery.Where(c => c.Local.Contains(local));
            }

            if (dataInicio.HasValue)
            {
                campanhasQuery = campanhasQuery.Where(c => c.DataInicio >= dataInicio.Value);
            }

            Campanhas = await campanhasQuery
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
                TempData["Erro"] = "You must be logged in as a volunteer to register..";
                return RedirectToPage();
            }

            var campanha = await _context.Campanhas
                .Include(c => c.CampanhaVoluntarios)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (campanha == null)
            {
                TempData["Erro"] = "The campaign does not exist.";
                return RedirectToPage();
            }

            if (campanha.DataFim < DateTime.Now)
            {
                TempData["Erro"] = "The campaign has now ended and can no longer receive entries..";
                return RedirectToPage();
            }

            if (campanha.CampanhaVoluntarios.Any(cv => cv.VoluntarioId == user.Id))
            {
                TempData["Erro"] = "You are already subscribed to this campaign.";
                return RedirectToPage();
            }

            if (campanha.CampanhaVoluntarios.Count >= campanha.NumeroMaximoVoluntarios)
            {
                TempData["Erro"] = "The campaign has already reached the maximum number of volunteers.";
                return RedirectToPage();
            }

            var inscricao = new CampanhaVoluntario
            {
                CampanhaId = campanha.Id,
                VoluntarioId = user.Id
            };

            _context.CampanhaVoluntarios.Add(inscricao);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Registration completed successfully!";
            return RedirectToPage();
        }


    }
}
