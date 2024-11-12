using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

namespace TicketResell_Service
{   
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<bool> AddFeedbackAsync(Feedback feedback) => await _feedbackRepository.AddAsync(feedback);

        public async Task<List<Feedback>> GetFeedbacksByTicketIdAsync(Guid ticketId) => await _feedbackRepository.GetFeedbacksByTicketId(ticketId);

        public async Task<List<Feedback>> GetFeedbackByUserIdAsync(Guid userId) => await _feedbackRepository.GetByUserIdAsync(userId);
    }
}
