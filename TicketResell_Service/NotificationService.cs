using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

namespace TicketResell_Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Notification> AddNotificationAsync(Guid? userId, string message) => await _notificationRepository.AddAsync(userId, message);

        public async Task<int> CountNotificationByUserIdAsync(Guid? userId) => await _notificationRepository.CountNotificationByUserId(userId);

        public async Task DeleteNotificationAsync(Guid notificationId) => await _notificationRepository.DeleteAsync(notificationId);

        public async Task<List<Notification>> GetNotificationsByUserIdAsync(Guid? userId) => await _notificationRepository.GetByUserIdAsync(userId);
    }
}
