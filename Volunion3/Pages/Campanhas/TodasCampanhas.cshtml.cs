using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volunion3.Models;
using Volunion3.Services;

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

        // Filtros
        public string Titulo { get; set; }
        public string Local { get; set; }
        public DateTime? DataInicio { get; set; }

        public async Task OnGetAsync(string titulo, string local, DateTime? dataInicio)
        {
            // Aplica os filtros na busca
            var campanhasQuery = _context.Campanhas.AsQueryable();

            // Adiciona filtros conforme o valor dos parâmetros
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

            // Carrega as campanhas com os filtros aplicados
            Campanhas = await campanhasQuery
                .Include(c => c.CampanhaVoluntarios) // Inclui as inscrições para poder contar
                .Include(c => c.Organizacao)         // Inclui a organização, se necessário
                .ToListAsync();

            // Verifica se o usuário logado é um voluntário
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
