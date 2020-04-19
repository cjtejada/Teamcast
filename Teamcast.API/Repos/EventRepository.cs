using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamcast.Data;
using Teamcast.Models;

namespace Teamcast.Repos
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext _eventContext;

        public EventRepository(DataContext dbContext)
        {
            _eventContext = dbContext;
        }

        public async Task<List<Event>> GetEvents(int userId, string sort, string search, double lat, double lon, double radius)
        {
            List<Event> events;

            Point location = new Point(lon, lat)
            {
                SRID = 4326
            };

            //Get user events
            if (sort == "desc" && userId != 0 && search == null)
                events = await _eventContext.Event
                        .Include(e => e.User)
                        .Include(e => e.EventMember)
                        .ThenInclude(em => em.User)
                        .Include(e => e.EventMember)
                        .ThenInclude(em => em.Team)
                        .ThenInclude(t => t.TeamMember)
                        .ThenInclude(tm => tm.User)
                        .Where(e => e.UserId == userId 
                        && e.Location.Distance(location) <= radius)
                        .OrderByDescending(e => e.CreatedDate)
                        .ToListAsync();
            else if (sort == "asc" && userId != 0 && search == null)
                events = await _eventContext.Event
                        .Include(e => e.User)
                        .Include(e => e.EventMember)
                        .ThenInclude(em => em.User)
                        .Include(e => e.EventMember)
                        .ThenInclude(em => em.Team)
                        .ThenInclude(t => t.TeamMember)
                        .ThenInclude(tm => tm.User)
                        .Where(e => e.UserId == userId 
                        && e.Location.Distance(location) <= radius)
                        .OrderBy(e => e.CreatedDate)
                        .ToListAsync();
            //Search user events
            else if (sort == "desc" && userId == 0 && search != null)
                events = await _eventContext.Event
                        .Include(e => e.User)
                        .Include(e => e.EventMember)
                        .ThenInclude(em => em.User)
                        .Include(e => e.EventMember)
                        .ThenInclude(em => em.Team)
                        .ThenInclude(t => t.TeamMember)
                        .ThenInclude(tm => tm.User)
                        .Where(e => e.UserId == userId 
                        && (e.Name.Contains(search) 
                        || e.Description.Contains(search))
                        && e.Location.Distance(location) <= radius)
                        .ToListAsync();
            //Search all near events
            else if (userId == 0 && search != null && lat != 0 && lon != 0)
                events = await _eventContext.Event
                        .Include(e => e.User)
                        .Include(e => e.EventMember)
                        .ThenInclude(em => em.User)
                        .Include(e => e.EventMember)
                        .ThenInclude(em => em.Team)
                        .ThenInclude(t => t.TeamMember)
                        .ThenInclude(tm => tm.User)
                        .Where(e => e.Name.Contains(search)
                        || e.Description.Contains(search)
                        && e.Location.Distance(location) <= radius)
                        .ToListAsync();
            //Get nearest event
            else if (userId == 0 && search == null && lat != 0 && lon != 0)
                events = await _eventContext.Event
                        .Include(e => e.User)
                        .Include(e => e.EventMember)
                        .ThenInclude(em => em.User)
                        .Include(e => e.EventMember)
                        .ThenInclude(em => em.Team)
                        .ThenInclude(t => t.TeamMember)
                        .ThenInclude(tm => tm.User)
                        .Where(e => e.Location.Distance(location) <= radius)
                        .ToListAsync();
            else
                events = await _eventContext.Event
                        .Include(e => e.User)
                        .Include(e => e.EventMember)
                        .ThenInclude(em => em.User)
                        .Include(e => e.EventMember)
                        .ThenInclude(em => em.Team)
                        .ThenInclude(t => t.TeamMember)
                        .ThenInclude(tm => tm.User)
                        .OrderBy(e => e.Id)
                        .ToListAsync();


            return events;
        }

        public async Task<Event> GetEvent(int id)
        {
            return await _eventContext.Event
                .Include(e => e.User)
                .Include(e => e.EventMember)
                .ThenInclude(em => em.User)
                .Include(e => e.EventMember)
                .ThenInclude(em => em.Team)
                .ThenInclude(t => t.TeamMember)
                .ThenInclude(tm => tm.User)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> JoinEvent(EventMember evMem)
        {
            await _eventContext.AddAsync(evMem);

            return await SaveChanges();
        }

        public async Task<bool> CreateEvent(int userId, Event ev)
        {
            ev.User = await _eventContext.User.FirstOrDefaultAsync(u => u.Id == userId);

            await _eventContext.Event.AddAsync(ev);

            return await SaveChanges();
        }

        public async Task<bool> DeleteEvent(Event ev)
        {
            _eventContext.Event.Remove(ev);

            return await SaveChanges();
        }

        public async Task<bool> EventExists(int eventId)
        {
            if (await _eventContext.Event.AnyAsync(e => e.Id == eventId))
                return true;

            return false;
        }

        public async Task<bool> UserIdExists(int userId)
        {
            if (!await _eventContext.User.AnyAsync(x => x.Id == userId))
                return false;

            return true;
        }

        public async Task<bool> IsEventMember(int userId, int eventId)
        {
            if (await _eventContext.EventMember.AnyAsync(x => x.UserId == userId && x.EventId == eventId))
                return true;

            return false;
        }

        public async Task<bool> SaveChanges()
        {
            return await _eventContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
