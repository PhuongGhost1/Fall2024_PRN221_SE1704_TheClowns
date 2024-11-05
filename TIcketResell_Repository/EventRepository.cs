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
        Task<EventType> GetEventById(Guid? id);
    }

    public class EventRepository : IEventRepository
    {
        public async Task<List<EventType>> GetAllEvents() => await EventDAO.GetInstance.GetAllEvents();

        public async Task<EventType> GetEventById(Guid? id) => await EventDAO.GetInstance.GetEventById(id);
    }
}