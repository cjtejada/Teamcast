using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamcast.Models;

namespace Teamcast.Repos
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetTeams();
        Task<Team> GetTeam(int id);
        Task<bool> JoinTeam(TeamMember evMem);
        Task<bool> CreateTeam(int userId, Team team);
        Task<bool> DeleteTeam(Team team);
        Task<bool> JoinEvent(EventMember evMem);
        Task<bool> SaveChanges();
        Task<bool> UserIdExists(int userId);
        Task<bool> IsTeamOwner(int userId);
        Task<bool> TeamExists(int teamId);
        Task<bool> IsTeamMember(int userId, int teamId);
    }
}
