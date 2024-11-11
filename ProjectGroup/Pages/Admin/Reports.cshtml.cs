using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectGroup.Pages.Admin
{
    public class ReportsModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReportsModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult OnGet()
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            string roleIdString = _httpContextAccessor.HttpContext.Session.GetString("RoleId");
            Guid? roleId = string.IsNullOrEmpty(roleIdString) ? (Guid?)null : Guid.Parse(roleIdString);

            if (!string.IsNullOrEmpty(username) && roleId != null)
            {
                if (roleId == Guid.Parse("D569ADDF-590D-4D0F-B17A-F153CDB5BA6A"))
                {
                    return RedirectToPage("/TicketsPage/Index");
                }
            }
            else
            {
                return RedirectToPage("/Authentication/Login");
            }

            return Page();
        }
    }
}
