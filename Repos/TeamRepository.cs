using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamcast.Data;
using Teamcast.Models;

namespace Teamcast.Repos
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext _dbContext;

        public TeamRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateTeam(int userId, Team team)
        {
            //team.User = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            await _dbContext.Teams.AddAsync(team);

            return await SaveChanges();
        }

        public async Task<bool> DeleteTeam(Team team)
        {
            var eventMem = await _dbContext.EventMembers
                .FirstOrDefaultAsync(t => t.TeamId == team.Id);

            if (eventMem != null)
                _dbContext.EventMembers.Remove(eventMem);

            _dbContext.Teams.Remove(team);

            return await SaveChanges();
        }

        public async Task<Team> GetTeam(int id)
        {
            return await _dbContext.Teams
                .Include(t => t.TeamMembers)
                .ThenInclude(tm => tm.User)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Team>> GetTeams()
        {
            return await _dbContext.Teams
                .Include(t => t.TeamMembers)
                .ThenInclude(tm => tm.User)
                .Include(t => t.User)
                .OrderBy(t => t.Id).ToListAsync();
        }

        public async Task<bool> IsTeamMember(int userId, int teamId)
        {
            if (await _dbContext.TeamMembers.AnyAsync(x => x.UserId == userId && x.TeamId == teamId))
                return true;

            return false;
        }

        public async Task<bool> JoinTeam(TeamMember teammem)
        {
            await _dbContext.AddAsync(teammem);

            return await SaveChanges();
        }

        public async Task<bool> JoinEvent(EventMember evMem)
        {
            await _dbContext.AddAsync(evMem);

            return await SaveChanges();
        }

        public async Task<bool> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<bool> TeamExists(int teamId)
        {
            if (await _dbContext.Teams.AnyAsync(e => e.Id == teamId))
                return true;

            return false;
        }

        public async Task<bool> UserIdExists(int userId)
        {
            if (!await _dbContext.Users.AnyAsync(x => x.Id == userId))
                return false;

            return true;
        }

        public async Task<bool> IsTeamOwner(int userId)
        {
            if (!await _dbContext.Teams.AnyAsync(x => x.UserId == userId))
                return false;

            return true;
        }
    }
}
