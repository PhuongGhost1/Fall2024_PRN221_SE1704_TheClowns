using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TIcketResell_Repository
{
    public interface IFeedbackRepository
    {
        Task<Feedback> GetByIdAsync(Guid id);
        Task<List<Feedback>> GetByUserIdAsync(Guid userId);
        Task<bool> AddAsync(Feedback feedback);
        Task<List<Feedback>> GetFeedbacksByTicketId(Guid ticketId);
    }
}
