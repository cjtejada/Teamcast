using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamcast.Models;

namespace Teamcast.Repos
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers(string userSearch);
        Task<User> GetUser(int id);
        Task<bool> SaveChanges();
        Task<bool> UserIdExists(int userId);
    }
}
