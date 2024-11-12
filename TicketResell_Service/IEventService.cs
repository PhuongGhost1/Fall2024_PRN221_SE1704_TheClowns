using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TicketResell_Service
{
    public interface IEventService
    {
        Task<List<EventType>> GetAllEventsAsync();
        Task<EventType?> GetEventByIdAsync(Guid? id);
        Task<bool> AddEventTypeAsync(EventType eventType);
        Task<bool> UpdateEventTypeAsync(EventType eventType);
        Task<bool> DeleteEventTypeAsync(Guid id);

        Task<bool> IsEventNameExistsAsync(string name);

    }
}
