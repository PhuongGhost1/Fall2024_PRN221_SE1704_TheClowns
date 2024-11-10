using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;

namespace TIcketResell_Repository
{
    public interface IFeedbackRepository
    {
        Task<Feedback> GetByIdAsync(Guid id);
        Task<List<Feedback>> GetByUserIdAsync(Guid userId);
        Task<bool> AddAsync(Feedback feedback);
        Task<List<Feedback>> GetFeedbacksByTicketId(Guid ticketId);
    }

    public class FeedbackRepository : IFeedbackRepository
    {
        public async Task<bool> AddAsync(Feedback feedback) => await FeedbackDAO.getInstance.AddFeedbackAsync(feedback);

        public async Task<Feedback> GetByIdAsync(Guid id) => await FeedbackDAO.getInstance.GetFeedbackByIdAsync(id);

        public async Task<List<Feedback>> GetByUserIdAsync(Guid userId) => await FeedbackDAO.getInstance.GetFeedbackByUserIdAsync(userId);

        public async Task<List<Feedback>> GetFeedbacksByTicketId(Guid ticketId) => await FeedbackDAO.getInstance.GetFeedbacksByTicketId(ticketId);
    }
}
