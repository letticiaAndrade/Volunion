using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Volunion3.Pages
{

    [Authorize(Roles = "voluntario")]
    public class VoluntarioModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
