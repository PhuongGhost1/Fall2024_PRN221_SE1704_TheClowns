using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TicketResell_Service
{
    public interface INotificationService
    {
        Task<List<Notification>> GetNotificationsByUserIdAsync(Guid? userId);
        Task<Notification> AddNotificationAsync(Guid? userId, string message);
        Task DeleteNotificationAsync(Guid notificationId);
        Task<int> CountNotificationByUserIdAsync(Guid? userId);
    }
}
