using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketResell_BusinessObject;
using TicketResell_Service;

namespace ProjectGroup.Pages.Customer
{
    public class NotificationsModel : PageModel
    {
        private readonly INotificationService _notificationService;
        public Guid? UserId { get; private set; }
        public Guid? RoleId { get; private set; }

        public List<Notification> Notifications { get; private set; }

        public NotificationsModel(IHttpContextAccessor httpContextAccessor, INotificationService notificationService)
        {
            string userIdString = httpContextAccessor.HttpContext.Session.GetString("UserId");
            string roleIdString = httpContextAccessor.HttpContext.Session.GetString("RoleId");

            UserId = string.IsNullOrEmpty(userIdString) ? (Guid?)null : Guid.Parse(userIdString);
            RoleId = string.IsNullOrEmpty(roleIdString) ? (Guid?)null : Guid.Parse(roleIdString);

            _notificationService = notificationService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (UserId != null && RoleId != null)
            {
                if (RoleId != Guid.Parse("57FA54D1-7C67-4405-B277-B8A58049FF3C"))
                {
                    return RedirectToPage("/TicketsPage/Index");
                }
            }
            else
            {
                return RedirectToPage("/Authentication/Login");
            }

            Notifications = await _notificationService.GetNotificationsByUserIdAsync(UserId);

            return Page();
        }
    }
}
