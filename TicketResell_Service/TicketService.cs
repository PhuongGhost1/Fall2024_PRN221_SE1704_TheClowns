﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;
using static TicketResell_DAO.TicketDAO;

namespace TicketResell_Service
{
        
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

                public async Task<List<Ticket>> GetTicketPending() => await _ticketRepository.GetTicketPending();

                public async Task<int> GetTicketCountAsync() => await _ticketRepository.CountAsync();

                public async Task<TicketPagination> GetTicketsAsync(string? query, Guid? eventTypeId, int currentPage, int pageSize) => await _ticketRepository.GetAllTickets(query, eventTypeId, currentPage, pageSize);

                public async Task<List<Ticket>> GetTicketsByOwnerIdAsync(Guid? ownerId) => await _ticketRepository.GetByOwnerIdAsync(ownerId);

                public async Task<List<Ticket>> SearchTicketsAsync(string eventName, DateTime? eventDate) => await _ticketRepository.SearchTicketsAsync(eventName, eventDate);

                public async Task<bool> UpdateTicketAsync(Ticket ticket) => await _ticketRepository.UpdateTicketStatus(ticket);

                public async Task<bool> TicketStatusUpdate(Guid ticketId, string Status) => await _ticketRepository.TicketStatusUpdate(ticketId, Status);
        }
}
