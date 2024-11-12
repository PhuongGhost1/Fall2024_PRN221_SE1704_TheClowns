using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;
using static TicketResell_DAO.TicketDAO;

namespace TIcketResell_Repository
{
    public class TicketRepository : ITicketRepository
    {
        public async Task<bool> AddAsync(Ticket ticket) => await TicketDAO.getInstance.AddTicketAsync(ticket);

        public async Task<int> CountAsync() => await TicketDAO.getInstance.CountTickets();


        public async Task<bool> DeleteAsync(Guid id) => await TicketDAO.getInstance.DeleteTicket(id);

        public async Task<List<Ticket>> GetByEventTypeAsync(Guid eventTypeId) => await TicketDAO.getInstance.GetTicketsByEventID(eventTypeId);

        public async Task<Ticket?> GetByIdAsync(Guid? id) => await TicketDAO.getInstance.GetTicketByID(id);

        public async Task<List<Ticket>> GetByOwnerIdAsync(Guid? ownerId) => await TicketDAO.getInstance.GetTicketsByOwnerID(ownerId);

        public async Task<List<Ticket>> GetTicketPending() => await TicketDAO.getInstance.GetTicketPending();

        public async Task<TicketPagination> GetAllTickets(string? query, Guid? eventTypeId, int currentPage, int pageSize) => await TicketDAO.getInstance.GetAllTickets(query, eventTypeId, currentPage, pageSize);

        public async Task<List<Ticket>> SearchTicketsAsync(string eventName, DateTime? eventDate) => await TicketDAO.getInstance.SearchTickets(eventName, eventDate);

        public async Task<bool> TicketStatusUpdate(Guid ticketId, string Status) => await TicketDAO.getInstance.TicketStatusUpdate(ticketId, Status);

        public async Task<bool> UpdateTicketStatus(Ticket ticket) => await TicketDAO.getInstance.UpdateTicketStatus(ticket);

    }
}
