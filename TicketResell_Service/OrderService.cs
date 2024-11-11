using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

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
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> CancelOrderAsync(Guid orderId) => await _orderRepository.CancelAsync(orderId);

        public async Task<Order> GetOrderByIdAsync(Guid id) => await _orderRepository.GetByIdAsync(id);

        public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId) => await _orderRepository.GetByUserIdAsync(userId);

        public async Task<bool> PlaceOrderAsync(Order order) => await _orderRepository.AddAsync(order);

        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, string status) => await _orderRepository.UpdateAsync(orderId, status);
    }
}