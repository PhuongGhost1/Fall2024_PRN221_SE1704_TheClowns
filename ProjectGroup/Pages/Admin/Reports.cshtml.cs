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
                if (roleId == Guid.Parse("57FA54D1-7C67-4405-B277-B8A58049FF3C"))
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
