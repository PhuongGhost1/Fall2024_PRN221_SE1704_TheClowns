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
        Task AddAsync(Feedback feedback);
    }

    public class FeedbackRepository : IFeedbackRepository
    {
        public async Task AddAsync(Feedback feedback) => await FeedbackDAO.getInstance.AddFeedbackAsync(feedback.UserId, feedback.Rating, feedback.Comment);

        public async Task<Feedback> GetByIdAsync(Guid id) => await FeedbackDAO.getInstance.GetFeedbackByIdAsync(id);

        public async Task<List<Feedback>> GetByUserIdAsync(Guid userId) => await FeedbackDAO.getInstance.GetFeedbackByUserIdAsync(userId);
    }
}
