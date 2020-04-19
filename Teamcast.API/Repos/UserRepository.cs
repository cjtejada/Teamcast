using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamcast.Data;
using Teamcast.Models;

namespace Teamcast.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _userContext;
        public UserRepository(DataContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<List<User>> GetUsers(string userSearch)
        {
            return await _userContext.User
                .Where(u => u.Name.Contains(userSearch) 
                || u.Lastname.Contains(userSearch) 
                || u.Username.Contains(userSearch))
                .ToListAsync(); 
        }

        public async Task<User> GetUser(int id)
        {
            return await _userContext.User
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _userContext.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<bool> UserIdExists(int userId)
        {
            if (!await _userContext.User.AnyAsync(x => x.Id == userId))
                return false;

            return true;
        }
    }
}
