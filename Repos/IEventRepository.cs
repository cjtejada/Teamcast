using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamcast.Models;

namespace Teamcast.Repos
{
    public interface IEventRepository
    {
        Task<List<Event>> GetEvents(int userId, string sort, string search, double lat, double lon, double radius);
        Task<Event> GetEvent(int id);
        Task<bool> JoinEvent(EventMember evMem);
        Task<bool> CreateEvent(int userId, Event ev);
        Task<bool> DeleteEvent(Event ev);
        Task<bool> SaveChanges();
        Task<bool> UserIdExists(int userId);
        Task<bool> EventExists(int eventId);
        Task<bool> IsEventMember(int userId, int eventId);
    }
}
