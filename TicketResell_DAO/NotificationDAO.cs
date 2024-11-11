using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;

namespace TicketResell_DAO
{
    public class NotificationDAO
    {
        private TicketResellContext _context;
        private static NotificationDAO _instance;

        public NotificationDAO()
        {
            _context = new TicketResellContext();
        }

        public static NotificationDAO getInstance
        {
            get{ 
                if(_instance == null)
                {
                    _instance = new NotificationDAO();
                }
                return _instance;
            }
        }

        public async Task<Notification> AddNotification(Guid? userId, string message)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                Message = message,
                SentAt = DateTime.UtcNow,
                UserId = userId
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<List<Notification>> GetNotificationsByUserID(Guid? userID)
        {
            return await _context.Notifications.Where(n => n.UserId == userID).ToListAsync();
        }

        public async Task<Notification> GetNotificationById(Guid id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<bool> DeleteNotification(Guid id)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);
            if(notification == null)
            {
                return false;
            }
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> CountNotificationByUserId(Guid? userId)
        {
            return await _context.Notifications.CountAsync(n => n.UserId == userId);
        }
    }
}
