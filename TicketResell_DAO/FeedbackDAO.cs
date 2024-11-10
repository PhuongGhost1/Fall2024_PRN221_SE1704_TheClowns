using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;

namespace TicketResell_DAO
{
    public class FeedbackDAO
    {
        private TicketResellContext _context;
        private static FeedbackDAO _instance;

        public FeedbackDAO()
        {
            _context = new TicketResellContext();
        }

        public static FeedbackDAO getInstance
        {
            get{ 
                if(_instance == null)
                {
                    _instance = new FeedbackDAO();
                }
                return _instance;
            }
        }

        public async Task<List<Feedback>> GetFeedbackByUserIdAsync(Guid userId)
        {
            return await _context.Feedbacks.Where(f => f.UserId == userId).ToListAsync();
        }

        public async Task<bool> AddFeedbackAsync(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
            return true;
        }

        public double? GetAverageRatingForSeller(Guid sellerID)
        {
            return _context.Feedbacks.Where(f => f.UserId == sellerID).Average(f => f.Rating);
        }

        public async Task<Feedback> GetFeedbackByIdAsync(Guid id)
        {
            return await _context.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Feedback>> GetFeedbacksByTicketId(Guid ticketId)
        {
            return await _context.Feedbacks
                .Where(f => f.TicketId == ticketId)
                .OrderByDescending(f => f.SubmittedAt)
                .Take(5)
                .ToListAsync();
        }
    }
}
