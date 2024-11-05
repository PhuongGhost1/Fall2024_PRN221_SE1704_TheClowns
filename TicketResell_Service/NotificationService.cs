using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

namespace TicketResell_Service
{
    public interface INotificationService
    {
        Task<List<Notification>> GetNotificationsByUserIdAsync(Guid userId);
        Task AddNotificationAsync(Guid userId, string message);
        Task DeleteNotificationAsync(Guid notificationId);
    }

    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task AddNotificationAsync(Guid userId, string message) => await _notificationRepository.AddAsync(new Notification { UserId = userId, Message = message });

        public async Task DeleteNotificationAsync(Guid notificationId) => await _notificationRepository.DeleteAsync(notificationId);

        public async Task<List<Notification>> GetNotificationsByUserIdAsync(Guid userId) => await _notificationRepository.GetByUserIdAsync(userId);
    }
}
