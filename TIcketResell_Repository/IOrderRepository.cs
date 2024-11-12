using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TIcketResell_Repository
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<List<Order>> GetByUserIdAsync(Guid userId);
        Task<bool> AddAsync(Order order);
        Task<bool> UpdateAsync(Guid orderId, string status);
        Task<bool> CancelAsync(Guid id);
    }
}
