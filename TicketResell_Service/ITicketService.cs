using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TicketResell_DAO.TicketDAO;
using TicketResell_BusinessObject;

namespace TicketResell_Service
{
    public interface ITicketService
    {
        Task<TicketPagination> GetTicketsAsync(string? query, Guid? eventTypeId, int currentPage, int pageSize);
        Task<Ticket?> GetTicketByIdAsync(Guid? id);
        Task<List<Ticket>> GetTicketsByOwnerIdAsync(Guid? ownerId);
        Task<List<Ticket>> GetByEventTypeAsync(Guid eventTypeId);
        Task<List<Ticket>> SearchTicketsAsync(string eventName, DateTime? eventDate);
        Task<bool> AddTicketAsync(Ticket ticket);
        Task<bool> UpdateTicketAsync(Ticket ticket);
        Task<bool> DeleteTicketAsync(Guid id);
        Task<List<Ticket>> GetTicketPending();
        Task<bool> TicketStatusUpdate(Guid ticketId, string Status);
        Task<int> GetTicketCountAsync();
    }
}
