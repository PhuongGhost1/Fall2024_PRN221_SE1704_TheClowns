using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;

namespace TicketResell_DAO
{
    public class OrderDAO
    {
        private TicketResellContext _context;
        private static OrderDAO _instance;
        
        public OrderDAO()
        {
            _context = new TicketResellContext();
        }

        public static OrderDAO getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderDAO();
                }
                return _instance;
            }
        }

        public async Task<bool> CreateOrder(Order order)
        {
           
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Order> GetOrderByID(Guid orderID)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderID);
        }

        public async Task<bool> UpdateOrderStatus(Guid orderId, string status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return false;
            }
            order.OrderStatus = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetOrdersByUserId(Guid userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task<bool> CancelOrder(Guid orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return false;
            }
            order.OrderStatus = "Cancelled";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}