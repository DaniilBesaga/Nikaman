using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Nikaman.Pages.Shared.Components.AdminPanelLogin
{
    public class AdminPanelDeniedViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AdminPanelDenied");
        }
        public void OnGet()
        {

        }
    }
}
