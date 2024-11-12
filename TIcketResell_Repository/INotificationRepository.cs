using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

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
}
