using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TicketResell_Service
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(Guid id);
        Task<List<Order>> GetOrdersByUserIdAsync(Guid userId);
        Task<bool> PlaceOrderAsync(Order order);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, string status);
        Task<bool> CancelOrderAsync(Guid orderId);
    }
}
