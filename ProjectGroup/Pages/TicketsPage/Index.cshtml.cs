using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;
using TicketResell_Service;

namespace ProjectGroup.Pages.TicketsPage
{
    public class IndexModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public List<Ticket> Tickets { get; set; } = default!;
        public Guid? UserId { get; set; }

        public IndexModel(ITicketService ticketService, IHttpContextAccessor httpContextAccessor)
        {
            _ticketService = ticketService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnGetAsync()
        {
            string userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            UserId = string.IsNullOrEmpty(userIdString) ? (Guid?)null : Guid.Parse(userIdString);

            Tickets = await _ticketService.GetTicketsAsync(UserId);
        }
    }
}
