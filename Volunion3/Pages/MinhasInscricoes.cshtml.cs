using Volunion3.Models;
using Volunion3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize(Roles = "voluntario")]
public class MinhasInscricoesModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public MinhasInscricoesModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<Campanha> Inscricoes { get; set; }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var campanhaVoluntario = await _context.CampanhasVoluntarios
            .FirstOrDefaultAsync(cv => cv.CampanhaId == id && cv.VoluntarioId == user.Id);

        if (campanhaVoluntario == null)
        {
            return NotFound();
        }

        _context.CampanhasVoluntarios.Remove(campanhaVoluntario);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            Inscricoes = new List<Campanha>();
            return;
        }

        Inscricoes = await _context.Campanhas
        .Include(c => c.CampanhaVoluntarios) 
        .Where(c => c.CampanhaVoluntarios.Any(v => v.VoluntarioId == user.Id))
        .ToListAsync();
    }
}
