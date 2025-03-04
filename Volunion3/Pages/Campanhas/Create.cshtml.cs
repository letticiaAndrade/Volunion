using Volunion3.Models;
using Volunion3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Volunion3.Pages.Campanhas
{
    public class CriarCampanhaModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CriarCampanhaModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Campanha Campanha { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || !await _userManager.IsInRoleAsync(user, "organizacao"))
            {
                return Forbid();
            }

            Campanha.OrganizacaoId = user.Id;
            Campanha.CreatedAt = DateTime.Now;

            _context.Campanhas.Add(Campanha);
            await _context.SaveChangesAsync();

            return RedirectToPage("/MinhasCampanhas");
        }
    }
}
