using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TicketResell_Service
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetFeedbackByUserIdAsync(Guid userId);
        Task<bool> AddFeedbackAsync(Feedback feedback);
        Task<List<Feedback>> GetFeedbacksByTicketIdAsync(Guid ticketId);
    }
}
