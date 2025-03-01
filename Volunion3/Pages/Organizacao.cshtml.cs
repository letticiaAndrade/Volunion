using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Volunion3.Pages
{
    [Authorize(Roles = "organizacao")]
    public class OrganizacaoModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
