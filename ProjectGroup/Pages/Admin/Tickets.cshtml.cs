using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketResell_BusinessObject;
using TicketResell_Service;

namespace ProjectGroup.Pages.Admin
{
    public class TicketsModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITicketService _ticketService;
        public TicketsModel(IHttpContextAccessor httpContextAccessor, ITicketService ticketService)
        {
            _httpContextAccessor = httpContextAccessor;
            _ticketService = ticketService;
        }
        [BindProperty]
        public List<Ticket> Tickets { get; set; }
        public async Task<IActionResult> OnGet()
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            string roleIdString = _httpContextAccessor.HttpContext.Session.GetString("RoleId");
            Guid? roleId = string.IsNullOrEmpty(roleIdString) ? (Guid?)null : Guid.Parse(roleIdString);

            if (!string.IsNullOrEmpty(username) && roleId != null)
            {
                if (roleId != Guid.Parse("D569ADDF-590D-4D0F-B17A-F153CDB5BA6A"))
                {
                    return RedirectToPage("/TicketsPage/Index");
                }
            }
            else
            {
                return RedirectToPage("/Authentication/Login");
            }

            Tickets = await _ticketService.GetTicketPending();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid ticketId, string action)
        {
            await _ticketService.TicketStatusUpdate(ticketId, action);
            Tickets = await _ticketService.GetTicketPending();
            return Page();
        }
    }
}
