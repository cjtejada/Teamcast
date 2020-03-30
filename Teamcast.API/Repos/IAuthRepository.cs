using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Teamcast.DTOs;
using Teamcast.Models;

namespace Teamcast.Repos
{
    public interface IAuthRepository
    {
        Task<User> Register(User user);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<User> GetUser(int userId);
        string Hash(string password);
        Task<bool> DeleteUser(User user);
        Task<bool> SaveChanges();
        //string GenerateToken(User user);
    }
}
