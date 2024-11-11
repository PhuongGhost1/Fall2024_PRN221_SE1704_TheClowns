using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;
using TicketResell_Service;
using static TicketResell_DAO.TicketDAO;

namespace ProjectGroup.Pages.TicketsPage
{
    public class IndexModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFeedbackService _feedbackService;
        private readonly IEventService _eventService;
        public List<Ticket> Tickets { get; set; } = default!;
        public List<EventType> EventTypes { get; set; }
        [BindProperty]
        public int currentPage { get; set; } = 1;
        [BindProperty]
        public int TotalPages { get; set; } = 0;
        public Dictionary<Guid, List<Feedback>> TicketFeedbacks { get; set; } = new Dictionary<Guid, List<Feedback>>();
        public Guid? UserId { get; set; }

        public IndexModel(ITicketService ticketService, IHttpContextAccessor httpContextAccessor, IFeedbackService feedbackService, IEventService eventService)
        {
            _ticketService = ticketService;
            _httpContextAccessor = httpContextAccessor;
            _feedbackService = feedbackService;
            _eventService = eventService;
        }

        public async Task OnGetAsync(int pageIndex = 1)
        {
            string userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            UserId = string.IsNullOrEmpty(userIdString) ? (Guid?)null : Guid.Parse(userIdString);
            TicketPagination ticketPagination = await _ticketService.GetTicketsAsync(null, null, pageIndex, 6);
            Tickets = ticketPagination.Tickets;
            TotalPages = ticketPagination.TotalPages;
            currentPage = ticketPagination.CurrentPage;
            EventTypes = await _eventService.GetAllEventsAsync();
            foreach (var ticket in Tickets)
            {
                var feedbacks = await _feedbackService.GetFeedbacksByTicketIdAsync(ticket.Id);
                TicketFeedbacks[ticket.Id] = feedbacks;
            }
        }

        public async Task<IActionResult> OnPostSubmitCommentAsync(Guid ticketId, string comment, int rating)
        {
            string userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            UserId = string.IsNullOrEmpty(userIdString) ? (Guid?)null : Guid.Parse(userIdString);

            if (UserId == null)
            {
                TempData["ErrorMessage"] = "You need to be logged in to submit feedback.";
                return RedirectToPage("/Authentication/Login");
            }

            var feedback = new Feedback
            {
                Id = Guid.NewGuid(),
                TicketId = ticketId,
                UserId = UserId,
                Comment = comment,
                Rating = rating,
                SubmittedAt = DateTime.Now
            };

            await _feedbackService.AddFeedbackAsync(feedback);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            string searchtxt = Request.Form["searchtxt"];
            TicketPagination ticketPagination;
            if (Guid.TryParse(Request.Form["eventSelect"].ToString(), out Guid eventSelect))
            {
                ticketPagination = await _ticketService.GetTicketsAsync(searchtxt, eventSelect, 1, 6);
            }
            else
            {
                ticketPagination = await _ticketService.GetTicketsAsync(searchtxt, null, 1, 6);
            }
            Tickets = ticketPagination.Tickets;
            TotalPages = ticketPagination.TotalPages;
            currentPage = ticketPagination.CurrentPage;
            EventTypes = await _eventService.GetAllEventsAsync();
            foreach (var ticket in Tickets)
            {
                var feedbacks = await _feedbackService.GetFeedbacksByTicketIdAsync(ticket.Id);
                TicketFeedbacks[ticket.Id] = feedbacks;
            }
            return Page();
        }
    }
}
