using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

namespace TicketResell_Service
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetFeedbackByUserIdAsync(Guid userId);
        Task AddFeedbackAsync(Guid userId, int rating, string comment);
    }
    
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task AddFeedbackAsync(Guid userId, int rating, string comment) => await _feedbackRepository.AddAsync(new Feedback { UserId = userId, Rating = rating, Comment = comment });

        public async Task<List<Feedback>> GetFeedbackByUserIdAsync(Guid userId) => await _feedbackRepository.GetByUserIdAsync(userId);
    }
}
