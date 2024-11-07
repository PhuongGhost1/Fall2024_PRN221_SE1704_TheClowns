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
        Task<List<Notification>> GetByUserIdAsync(Guid? userId);
        Task<Notification> AddAsync(Guid? userId, string message);
        Task<bool> DeleteAsync(Guid id);
        Task<int> CountNotificationByUserId(Guid? userId);
    }

    public class NotificationRepository : INotificationRepository
    {
        public async Task<Notification> AddAsync(Guid? userId, string message) => await NotificationDAO.getInstance.AddNotification(userId, message);

        public async Task<int> CountNotificationByUserId(Guid? userId) => await NotificationDAO.getInstance.CountNotificationByUserId(userId);

        public async Task<bool> DeleteAsync(Guid id) => await NotificationDAO.getInstance.DeleteNotification(id);

        public async Task<Notification> GetByIdAsync(Guid id) => await NotificationDAO.getInstance.GetNotificationById(id);

        public async Task<List<Notification>> GetByUserIdAsync(Guid? userId) => await NotificationDAO.getInstance.GetNotificationsByUserID(userId);
    }
}
