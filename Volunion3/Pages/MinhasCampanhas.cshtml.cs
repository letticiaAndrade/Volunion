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
        var campanha = await _context.Campanhas.FindAsync(id);

        if (campanha == null)
        {
            return NotFound();
        }

        _context.Campanhas.Remove(campanha);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

        public async Task OnGetAsync()
        {
            // Obtenha o usu�rio logado
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                // Verifique se o usu�rio tem campanhas associadas
                Campanhas = await _context.Campanhas
                     .Include(c => c.CampanhaVoluntarios)
                    .Where(c => c.OrganizacaoId == user.Id) // Ajuste isso conforme sua l�gica de neg�cio
                    .ToListAsync();
            }
            else
            {
                Campanhas = new List<Campanha>(); // Caso n�o tenha campanhas ou o usu�rio n�o seja encontrado
            }
        }
    }


