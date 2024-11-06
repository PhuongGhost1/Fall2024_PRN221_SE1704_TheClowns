using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;

namespace TicketResell_DAO
{
    public class TicketDAO
    {
        private TicketResellContext _context;
        private static TicketDAO _instance;

        public TicketDAO()
        {
            _context = new TicketResellContext();
        }

        public static TicketDAO getInstance
        {
            get{ 
                if(_instance == null)
                {
                    _instance = new TicketDAO();
                }
                return _instance;
            }
        }

        public async Task<List<Ticket>> GetAllTickets(Guid? userId)
        {
            return await _context.Tickets
                            .Where(t => t.TicketStatus == "Available" || t.TicketStatus == "Sold")
                            .Include(t => t.EventType)
                            .Include(t => t.Owner)
                            .Include(t => t.Images)
                            .ToListAsync();
        }

        public async Task<bool> AddTicketAsync(Ticket ticket)
        {
            var newTicket = new Ticket
            {
                Id = Guid.NewGuid(),
                EventName = ticket.EventName,
                EventDate = ticket.EventDate,
                EventTypeId = ticket.EventTypeId,
                OwnerId = ticket.OwnerId,
                TicketStatus = "Pending",
                Price = ticket.Price,
                ExpirationDate = ticket.ExpirationDate,
                SubmittedAt = DateTime.Now,
                Serial = ticket.Serial,
                Images = ticket.Images
            };

            await _context.Tickets.AddAsync(newTicket);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Ticket?> GetTicketByID(Guid? ticketID)
        {
            return await _context.Tickets.Where(t => t.Id == ticketID)
                                .Include(t => t.Images)
                                .FirstOrDefaultAsync();
        }

        public async Task<List<Ticket>> GetTicketsByEventID(Guid eventID)
        {
            return await _context.Tickets.Where(t => t.EventTypeId == eventID).ToListAsync();
        }

        public async Task<bool> UpdateTicketStatus(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Ticket>> GetTicketsByOwnerID(Guid? ownerID)
        {
            return await _context.Tickets.Where(t => t.OwnerId == ownerID)
                                        .Include(t => t.Images)
                                        .ToListAsync();
        }

        public Task<List<Ticket>> SearchTickets(string eventName, DateTime? eventDate)
        {
            var tickets = _context.Tickets.Where(t => t.EventName.Contains(eventName) && t.EventDate == eventDate).ToListAsync();
            return tickets;
        }

        public async Task<bool> DeleteTicket(Guid ticketID)
        {
            var ticket = await _context.Tickets.FindAsync(ticketID);
            if(ticket == null)
            {
                return false;
            }
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }      
    }
}
