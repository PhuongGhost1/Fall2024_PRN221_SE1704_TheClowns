using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TIcketResell_Repository
{
    public interface IEventRepository
    {
        Task<List<EventType>> GetAllEvents();
        Task<EventType?> GetEventById(Guid? id);
        Task<bool> AddEventType(EventType eventType);
        Task<bool> UpdateEventType(EventType eventType);
        Task<bool> DeleteEventType(Guid? id);

        Task<bool> IsEventNameExistsAsync(string name);
    }
}
