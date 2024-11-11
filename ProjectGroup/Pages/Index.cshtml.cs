using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketResell_BusinessObject;
using TicketResell_Service;

namespace ProjectGroup.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITicketService _ticketService;

        [BindProperty]
        public List<Ticket> Tickets { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
            Tickets = new List<Ticket>();
        }

        public async void OnGet()
        {
            //Tickets = await _ticketService.GetTicketsAsync();
        }
    }
}
