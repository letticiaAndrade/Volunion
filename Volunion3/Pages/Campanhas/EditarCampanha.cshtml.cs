using Volunion3.Models;
using System.Diagnostics;
using Volunion3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                Debug.WriteLine("Campanha n�o encontrada.");
                return NotFound();
            }

            Debug.WriteLine($"Campanha encontrada: {Campanha.Titulo}");
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            Debug.WriteLine("Cheguei no OnPostUpdateAsync!");

            var campanhaDb = await _context.Campanhas
                                            .FirstOrDefaultAsync(c => c.Id == Campanha.Id);

            if (campanhaDb == null)
            {
                Debug.WriteLine($"Campanha com ID {Campanha.Id} n�o encontrada.");
                return NotFound();
            }

            campanhaDb.Titulo = Campanha.Titulo;
            campanhaDb.Descricao = Campanha.Descricao;
            campanhaDb.Local = Campanha.Local;
            campanhaDb.DataInicio = Campanha.DataInicio;
            campanhaDb.DataFim = Campanha.DataFim;
            campanhaDb.NumeroMaximoVoluntarios = Campanha.NumeroMaximoVoluntarios;

            campanhaDb.OrganizacaoId = Campanha.OrganizacaoId;

            var organizacao = await _context.Users.FindAsync(Campanha.OrganizacaoId);
            if (organizacao == null)
            {
                ModelState.AddModelError("OrganizacaoId", "Organiza��o n�o encontrada.");
                Debug.WriteLine("Organiza��o n�o encontrada.");
                return Page();
            }

            try
            {
                Debug.WriteLine("Tentando salvar as altera��es...");
                await _context.SaveChangesAsync();
                Debug.WriteLine("Altera��es salvas com sucesso.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao salvar as altera��es: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Erro ao salvar a campanha.");
                return Page();
            }

            return RedirectToPage("/MinhasCampanhas");
        }



    }
}
