using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketResell_BusinessObject;
using TicketResell_Service;

namespace ProjectGroup.Pages.Customer
{
    public class ProfileModel : PageModel
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;
        private readonly ISocialMediaService _socialMediaService;

        [BindProperty]
        public User Customer { get; set; }

        [BindProperty]
        public List<Transactions> Transactions { get; set; }

        [BindProperty]
        public List<Ticket> PostedTickets { get; set; }

        [BindProperty]
        public string FacebookLink { get; set; }

        [BindProperty]
        public string TwitterLink { get; set; }

        [BindProperty]
        public string InstagramLink { get; set; }

        [BindProperty]
        public string CurrentPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string EditUserName { get; set; }

        [BindProperty]
        public string UserEmail { get; set; }

        [BindProperty]
        public string EditUserPhone { get; set; }

        public ProfileModel(ITicketService ticketService, IUserService userService, ITransactionService transactionService,
            IHttpContextAccessor contextAccessor, ISocialMediaService socialMediaService)
        {
            _ticketService = ticketService;
            _userService = userService;
            _transactionService = transactionService;
            _contextAccessor = contextAccessor;
            _socialMediaService = socialMediaService;
        }

        private async Task LoadDataAsync()
        {
            string username = _contextAccessor.HttpContext.Session.GetString("UserName");
            string userIdString = _contextAccessor.HttpContext.Session.GetString("UserId");
            string roleIdString = _contextAccessor.HttpContext.Session.GetString("RoleId");

            Guid? roleId = string.IsNullOrEmpty(roleIdString) ? (Guid?)null : Guid.Parse(roleIdString);
            Guid? userId = string.IsNullOrEmpty(userIdString) ? (Guid?)null : Guid.Parse(userIdString);

            if (!string.IsNullOrEmpty(username) && roleId != null && userId != null)
            {
                Transactions = await _transactionService.GetTransactionsByUserIdAsync(userId);
                PostedTickets = await _ticketService.GetTicketsByOwnerIdAsync(userId);

                var socialMediaList = await _socialMediaService.GetSocialMediaByUserId(userId);
                FacebookLink = socialMediaList.FirstOrDefault(s => s.Type.Trim() == "Facebook")?.Link;
                TwitterLink = socialMediaList.FirstOrDefault(s => s.Type.Trim() == "Twitter")?.Link;
                InstagramLink = socialMediaList.FirstOrDefault(s => s.Type.Trim() == "Instagram")?.Link;

                Customer = await _userService.GetUserByIdAsync(userId);
                CurrentPassword = Customer.Password;
                EditUserName = Customer.Username;
                UserEmail = Customer.Email;
                EditUserPhone = Customer.Phone;
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string username = _contextAccessor.HttpContext.Session.GetString("UserName");
            string userIdString = _contextAccessor.HttpContext.Session.GetString("UserId");
            string roleIdString = _contextAccessor.HttpContext.Session.GetString("RoleId");

            Guid? roleId = string.IsNullOrEmpty(roleIdString) ? (Guid?)null : Guid.Parse(roleIdString);
            Guid? userId = string.IsNullOrEmpty(userIdString) ? (Guid?)null : Guid.Parse(userIdString);

            if (!string.IsNullOrEmpty(username) && roleId != null && userId != null)
            {
                if (roleId != Guid.Parse("57FA54D1-7C67-4405-B277-B8A58049FF3C"))
                {
                    return RedirectToPage("/TicketsPage/Index");
                }

                Transactions = await _transactionService.GetTransactionsByUserIdAsync(userId);
                PostedTickets = await _ticketService.GetTicketsByOwnerIdAsync(userId);

                var socialMediaList = await _socialMediaService.GetSocialMediaByUserId(userId);
                FacebookLink = socialMediaList.FirstOrDefault(s => s.Type.Trim() == "Facebook")?.Link;
                TwitterLink = socialMediaList.FirstOrDefault(s => s.Type.Trim() == "Twitter")?.Link;
                InstagramLink = socialMediaList.FirstOrDefault(s => s.Type.Trim() == "Instagram")?.Link;

                Customer = await _userService.GetUserByIdAsync(userId);
                CurrentPassword = Customer.Password;
                EditUserName = Customer.Username;
                UserEmail = Customer.Email;
                EditUserPhone = Customer.Phone;
            }
            else
            {
                return RedirectToPage("/Authentication/Login");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync()
        {
            var userIdString = _contextAccessor.HttpContext.Session.GetString("UserId");
            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                TempData["ErrorMessage"] = "User not found. Please log in again.";
                return RedirectToPage("/Authentication/Login");
            }

            if (string.IsNullOrEmpty(EditUserName) || string.IsNullOrEmpty(EditUserPhone))
            {
                await LoadDataAsync();
                ModelState.AddModelError(string.Empty, "All profile fields are required.");
                TempData["ErrorMessage"] = "All profile fields are required.";
                return Page();
            }

            if (EditUserPhone.Length < 9 || EditUserPhone.Length > 12 || !Regex.IsMatch(EditUserPhone, @"^\d{9,12}$"))
            {
                if (!Regex.IsMatch(EditUserPhone, @"^\d{9,12}$"))
                {
                    await LoadDataAsync();
                    ModelState.AddModelError("EditUserPhone", "Phone number must contain only digits and be between 9 and 12 characters.");
                    TempData["ErrorMessage"] = "Phone number must contain only digits and be between 9 and 12 characters.";
                    return Page();
                }
                else
                {
                    await LoadDataAsync();
                    ModelState.AddModelError("EditUserPhone", "Phone number must be between 9 and 12 digits.");
                    TempData["ErrorMessage"] = "Phone number must be between 9 and 12 digits.";
                    return Page();
                }

            }

            var existingUser = await _userService.GetUserByIdAsync(userId);

            if (existingUser == null)
            {
                TempData["ErrorMessage"] = "User not found. Please log in again.";
                return RedirectToPage("/Authentication/Login");
            }

            existingUser.Username = EditUserName.Trim();
            existingUser.Phone = EditUserPhone.Trim();

            bool updateSuccessful = await _userService.UpdateUserAsync(existingUser);
            if (!updateSuccessful)
            {
                await LoadDataAsync();
                TempData["ErrorMessage"] = "Failed to update the profile. Please try again.";
                return Page();
            }

            await LoadDataAsync();
            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            var userIdString = _contextAccessor.HttpContext.Session.GetString("UserId");
            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                TempData["ErrorMessage"] = "User not found. Please log in again.";
                return RedirectToPage("/Authentication/Login");
            }

            if (string.IsNullOrEmpty(CurrentPassword) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
            {
                ModelState.AddModelError(string.Empty, "All password fields are required.");
                return Page();
            }

            if (NewPassword.Trim() != ConfirmPassword.Trim())
            {
                ModelState.AddModelError("ConfirmPassword", "New password and confirmation do not match.");
                return Page();
            }

            bool isCurrentPasswordValid = await _userService.ValidatePasswordAsync(userId, CurrentPassword.Trim());
            if (!isCurrentPasswordValid)
            {
                ModelState.AddModelError("CurrentPassword", "Current password is incorrect.");
                return Page();
            }

            bool updateSuccessful = await _userService.SaveNewPassword(userId, NewPassword.Trim());
            if (!updateSuccessful)
            {
                ModelState.AddModelError(string.Empty, "Failed to update the password. Please try again.");
                return Page();
            }

            TempData["SuccessMessage"] = "Password updated successfully!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateSocialMediaAsync()
        {
            var userIdString = _contextAccessor.HttpContext.Session.GetString("UserId");
            if (Guid.TryParse(userIdString, out Guid userId))
            {
                await _socialMediaService.UpdateSocialMediaLinksAsync(userId, FacebookLink, TwitterLink, InstagramLink);
                TempData["SuccessMessage"] = "Social media links updated successfully!";
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostHideTicketAsync(Guid? id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
            {
                TempData["ErrorMessage"] = "Ticket not found!";
                return Page();
            }

            ticket.TicketStatus = "Hide";

            await _ticketService.UpdateTicketAsync(ticket);
            TempData["SuccessMessage"] = "Updated ticket to invisible successfully!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAvailableTicketAsync(Guid? id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
            {
                TempData["ErrorMessage"] = "Ticket not found!";
                return Page();
            }

            ticket.TicketStatus = "Available";

            await _ticketService.UpdateTicketAsync(ticket);
            TempData["SuccessMessage"] = "Updated ticket to visible successfully!";
            return RedirectToPage();
        }
    }
}
