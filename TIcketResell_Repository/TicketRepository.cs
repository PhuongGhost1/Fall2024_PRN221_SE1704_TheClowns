using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;

namespace TIcketResell_Repository
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetTickets(Guid? userId);
        Task<Ticket?> GetByIdAsync(Guid? id);
        Task<List<Ticket>> GetByOwnerIdAsync(Guid? ownerId);
        Task<List<Ticket>> GetByEventTypeAsync(Guid eventTypeId);
        Task<List<Ticket>> SearchTicketsAsync(string eventName, DateTime? eventDate);
        Task<bool> AddAsync(Ticket ticket);
        Task<bool> UpdateTicketStatus(Ticket ticket);
        Task<bool> DeleteAsync(Guid id);
    }
    public class TicketRepository : ITicketRepository
    {
        public async Task<bool> AddAsync(Ticket ticket) => await TicketDAO.getInstance.AddTicketAsync(ticket);

        public async Task<bool> DeleteAsync(Guid id) => await TicketDAO.getInstance.DeleteTicket(id);

        public async Task<List<Ticket>> GetByEventTypeAsync(Guid eventTypeId) => await TicketDAO.getInstance.GetTicketsByEventID(eventTypeId);

        public async Task<Ticket?> GetByIdAsync(Guid? id) => await TicketDAO.getInstance.GetTicketByID(id);

        public async Task<List<Ticket>> GetByOwnerIdAsync(Guid? ownerId) => await TicketDAO.getInstance.GetTicketsByOwnerID(ownerId);

        public async Task<List<Ticket>> GetTickets(Guid? userId) => await TicketDAO.getInstance.GetAllTickets(userId);

        public async Task<List<Ticket>> SearchTicketsAsync(string eventName, DateTime? eventDate) => await TicketDAO.getInstance.SearchTickets(eventName, eventDate);

        public async Task<bool> UpdateTicketStatus(Ticket ticket) => await TicketDAO.getInstance.UpdateTicketStatus(ticket);
    }
}
