using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

namespace TicketResell_Service
{
    public interface IEventService
    {
        Task<List<EventType>> GetAllEventsAsync();
        Task<EventType> GetEventByIdAsync(Guid? id);   
    }
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<List<EventType>> GetAllEventsAsync() => await _eventRepository.GetAllEvents();

        public async Task<EventType> GetEventByIdAsync(Guid? id) => await _eventRepository.GetEventById(id);
    }
}