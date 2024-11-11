using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;

namespace TIcketResell_Repository
{
    public interface IEventRepository
    {
        Task<List<EventType>> GetAllEvents();
        Task<EventType?> GetEventById(Guid? id);
        Task<bool> AddEventType(EventType eventType);
        Task<bool> UpdateEventType(EventType eventType);
        Task<bool> DeleteEventType(Guid? id);
    }

    public class EventRepository : IEventRepository
    {
        public async Task<bool> AddEventType(EventType eventType) => await EventDAO.GetInstance.AddEventType(eventType);

        public async Task<bool> DeleteEventType(Guid? id) => await EventDAO.GetInstance.DeleteEventType(id);

        public async Task<List<EventType>> GetAllEvents() => await EventDAO.GetInstance.GetAllEvents();

        public async Task<EventType?> GetEventById(Guid? id) => await EventDAO.GetInstance.GetEventById(id);

        public async Task<bool> UpdateEventType(EventType eventType) => await EventDAO.GetInstance.UpdateEventType(eventType);
    }
}