using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;

namespace TicketResell_DAO
{
    public class EventDAO
    {
        private readonly TicketResellContext _context;
        public static EventDAO _instance;

        public EventDAO(TicketResellContext context)
        {
            _context = context;
        }

        public static EventDAO GetInstance
        {
           get => _instance = _instance ?? new EventDAO(new TicketResellContext());
        }

        public async Task<List<EventType>> GetAllEvents()
        {
            return await _context.EventTypes.ToListAsync();
        }

        public async Task<EventType> GetEventById(Guid? id)
        {
            return await _context.EventTypes.FindAsync(id);
        }
    }
}