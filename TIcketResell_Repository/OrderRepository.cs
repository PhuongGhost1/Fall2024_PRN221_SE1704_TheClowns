using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;

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
    public class OrderRepository : IOrderRepository
    {
        public async Task<bool> AddAsync(Order order) => await OrderDAO.getInstance.CreateOrder(order);

        public async Task<bool> CancelAsync(Guid id) => await OrderDAO.getInstance.CancelOrder(id);

        public async Task<Order> GetByIdAsync(Guid id) => await OrderDAO.getInstance.GetOrderByID(id);

        public async Task<List<Order>> GetByUserIdAsync(Guid userId) => await OrderDAO.getInstance.GetOrdersByUserId(userId);

        public async Task<bool> UpdateAsync(Guid orderId, string status) => await OrderDAO.getInstance.UpdateOrderStatus(orderId, status);
    }
}