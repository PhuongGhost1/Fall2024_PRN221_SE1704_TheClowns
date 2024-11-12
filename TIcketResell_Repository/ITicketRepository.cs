using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TicketResell_DAO.TicketDAO;
using TicketResell_BusinessObject;

namespace TIcketResell_Repository
{
    public interface ITicketRepository
    {
        Task<TicketPagination> GetAllTickets(string? query, Guid? eventTypeId, int currentPage, int pageSize);
        Task<Ticket?> GetByIdAsync(Guid? id);
        Task<List<Ticket>> GetByOwnerIdAsync(Guid? ownerId);
        Task<List<Ticket>> GetByEventTypeAsync(Guid eventTypeId);
        Task<List<Ticket>> SearchTicketsAsync(string eventName, DateTime? eventDate);
        Task<bool> AddAsync(Ticket ticket);
        Task<bool> UpdateTicketStatus(Ticket ticket);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Ticket>> GetTicketPending();
        Task<bool> TicketStatusUpdate(Guid ticketId, string Status);
        Task<int> CountAsync();
    }
}
