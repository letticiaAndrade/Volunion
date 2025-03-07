using Volunion3.Models;
using Volunion3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize(Roles = "organizacao")] 
public class MinhasCampanhasModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public MinhasCampanhasModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<Campanha> Campanhas { get; set; }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var campanha = await _context.Campanhas
            .Include(c => c.CampanhaVoluntarios) 
            .Include(c => c.Avaliacoes)         
            .FirstOrDefaultAsync(c => c.Id == id);

        if (campanha == null)
        {
            return NotFound();
        }

        _context.CampanhasVoluntarios.RemoveRange(campanha.CampanhaVoluntarios);

        _context.Avaliacoes.RemoveRange(campanha.Avaliacoes);

        _context.Campanhas.Remove(campanha);

        await _context.SaveChangesAsync();

        TempData["Sucesso"] = "Campaign successfully deleted!";
        return RedirectToPage();
    }

    public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                Campanhas = await _context.Campanhas
                     .Include(c => c.CampanhaVoluntarios)
                    .Where(c => c.OrganizacaoId == user.Id) 
                    .ToListAsync();
            }
            else
            {
                Campanhas = new List<Campanha>();
            }
        }
    }


