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
        private readonly IFeedbackService _feedbackService;
        public List<Ticket> Tickets { get; set; } = default!;
        public Dictionary<Guid, List<Feedback>> TicketFeedbacks { get; set; } = new Dictionary<Guid, List<Feedback>>();
        public Guid? UserId { get; set; }

        public IndexModel(ITicketService ticketService, IHttpContextAccessor httpContextAccessor, IFeedbackService feedbackService)
        {
            _ticketService = ticketService;
            _httpContextAccessor = httpContextAccessor;
            _feedbackService = feedbackService;
        }

        public async Task OnGetAsync()
        {
            string userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            UserId = string.IsNullOrEmpty(userIdString) ? (Guid?)null : Guid.Parse(userIdString);

            Tickets = await _ticketService.GetTicketsAsync();
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
    }
}
