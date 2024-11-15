using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

namespace TicketResell_Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<bool> AddEventTypeAsync(EventType eventType) => await _eventRepository.AddEventType(eventType);

        public async Task<bool> DeleteEventTypeAsync(Guid id) => await _eventRepository.DeleteEventType(id);

        public async Task<List<EventType>> GetAllEventsAsync() => await _eventRepository.GetAllEvents();

        public async Task<EventType?> GetEventByIdAsync(Guid? id) => await _eventRepository.GetEventById(id);

        public async Task<bool> UpdateEventTypeAsync(EventType eventType) => await _eventRepository.UpdateEventType(eventType);


        public async Task<bool> IsEventNameExistsAsync(string name)
        {
            return await _eventRepository.IsEventNameExistsAsync(name);
        }

    }
}