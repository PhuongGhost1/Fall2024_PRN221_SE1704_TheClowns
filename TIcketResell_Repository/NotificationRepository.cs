using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;

namespace TIcketResell_Repository
{
    public interface INotificationRepository
    {
        Task<Notification> GetByIdAsync(Guid id);
        Task<List<Notification>> GetByUserIdAsync(Guid userId);
        Task<bool> AddAsync(Notification notification);
        Task<bool> DeleteAsync(Guid id);
    }

    public class NotificationRepository : INotificationRepository
    {
        public async Task<bool> AddAsync(Notification notification) => await NotificationDAO.getInstance.AddNotification(notification);

        public async Task<bool> DeleteAsync(Guid id) => await NotificationDAO.getInstance.DeleteNotification(id);

        public async Task<Notification> GetByIdAsync(Guid id) => await NotificationDAO.getInstance.GetNotificationById(id);

        public async Task<List<Notification>> GetByUserIdAsync(Guid userId) => await NotificationDAO.getInstance.GetNotificationsByUserID(userId);
    }
}
