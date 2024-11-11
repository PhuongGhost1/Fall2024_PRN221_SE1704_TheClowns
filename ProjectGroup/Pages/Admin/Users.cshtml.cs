using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;
using TicketResell_BusinessObject;
using TicketResell_Service;

namespace ProjectGroup.Pages.Admin
{
    public class UsersModel : PageModel

    {

        public List<User> Users { get; set; }

        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersModel(IHttpContextAccessor httpContextAccessor,IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;

            _userService = userService; 
        }
        public async Task<IActionResult> OnGetAsync()
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
            Users = await _userService.GetAllUsersAsync();

            return Page();

        }

        public async Task<IActionResult> OnPostToggleStatusAsync(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user != null)
            {

                if (user.Status == "Active") {
                    user.Status = "Blocked";
                }
                else
                {
                    user.Status = "Active";
                }

                await _userService.UpdateUserAsync(user); 
            }
            return RedirectToPage();
        }
    }
}
