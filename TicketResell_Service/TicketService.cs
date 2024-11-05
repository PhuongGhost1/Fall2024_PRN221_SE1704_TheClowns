using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

namespace TicketResell_Service
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetTicketsAsync(Guid? userId);
        Task<Ticket?> GetTicketByIdAsync(Guid? id);
        Task<List<Ticket>> GetTicketsByOwnerIdAsync(Guid? ownerId);
        Task<List<Ticket>> GetByEventTypeAsync(Guid eventTypeId);
        Task<List<Ticket>> SearchTicketsAsync(string eventName, DateTime? eventDate);
        Task<bool> AddTicketAsync(Ticket ticket);
        Task<bool> UpdateTicketAsync(Ticket ticket);
        Task<bool> DeleteTicketAsync(Guid id);
    }
    public class TicketService : ITicketService
    {
        private ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;   
        }

        public async Task<bool> AddTicketAsync(Ticket ticket) => await _ticketRepository.AddAsync(ticket);

        public async Task<bool> DeleteTicketAsync(Guid id) => await _ticketRepository.DeleteAsync(id);

        public async Task<List<Ticket>> GetByEventTypeAsync(Guid eventTypeId) => await _ticketRepository.GetByEventTypeAsync(eventTypeId);

        public async Task<Ticket?> GetTicketByIdAsync(Guid? id) => await _ticketRepository.GetByIdAsync(id);

        public async Task<List<Ticket>> GetTicketsAsync(Guid? userId) => await _ticketRepository.GetTickets(userId);

        public async Task<List<Ticket>> GetTicketsByOwnerIdAsync(Guid? ownerId) => await _ticketRepository.GetByOwnerIdAsync(ownerId);

        public async Task<List<Ticket>> SearchTicketsAsync(string eventName, DateTime? eventDate) => await _ticketRepository.SearchTicketsAsync(eventName, eventDate);

        public async Task<bool> UpdateTicketAsync(Ticket ticket) => await _ticketRepository.UpdateTicketStatus(ticket);

    }
}
