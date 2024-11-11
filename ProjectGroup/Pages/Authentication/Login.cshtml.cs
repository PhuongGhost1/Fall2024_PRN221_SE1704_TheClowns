using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketResell_Service;
using TicketResell_Service.Utilities;

namespace ProjectGroup.Pages.Authentication
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            string email = Request.Form["txtUsername"];
            string password = Request.Form["txtPwd"];

            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Email or Password is wrong!";
                return RedirectToPage("/Authentication/Login");
            }

            string hashedPassword = SecurityHelper.HashPassword(password);
            if (!hashedPassword.Equals(user.Password.Trim()))
            {
                TempData["ErrorMessage"] = "Email or Password is wrong!";
                return RedirectToPage("/Authentication/Login");
            }

            if(user.Status == "Blocked")
            {
                TempData["ErrorMessage"] = "Your account is blocked!";
                return RedirectToPage("/Authentication/Login");
            }

            var userRole = await _userService.GetUserRoleAsync(user.Id);
            if (userRole == null) return Redirect("/Error");

            Guid? roleId = userRole.RoleId;
            Guid? userId = userRole.UserId;
            string userName = user.Username;
            decimal? balance = user.Wallet;

            HttpContext.Session.SetString("RoleId", roleId.ToString());
            HttpContext.Session.SetString("UserId", userId.ToString());
            HttpContext.Session.SetString("UserName", userName);
            HttpContext.Session.SetString("Balance", balance.ToString());

            string redirectUrl = roleId switch
            {
                var r when r == Guid.Parse("51FABC7A-D801-429E-AA1E-6452EA780483") => "/Admin/Dashboard", //Admin
                var r when r == Guid.Parse("57FA54D1-7C67-4405-B277-B8A58049FF3C") => "/TicketsPage", //Customer
                var r when r == Guid.Parse("D569ADDF-590D-4D0F-B17A-F153CDB5BA6A") => "/Admin/Dashboard", //Staff
                _ => "/TicketsPage"
            };

            return Redirect(redirectUrl);
        }
    }
}
