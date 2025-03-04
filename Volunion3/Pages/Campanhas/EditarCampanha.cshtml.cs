using Volunion3.Models;
using Volunion3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Volunion3.Pages.Campanhas
{
    public class EditarCampanhaModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditarCampanhaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Campanha Campanha { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Campanha = await _context.Campanhas.FindAsync(id);

            if (Campanha == null)
            {
                return NotFound();
            }

            return Page();
        }
                public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Campanhas.Update(Campanha);
            await _context.SaveChangesAsync();

            return RedirectToPage("/MinhasCampanhas");
        }
    }
}
