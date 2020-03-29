using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Teamcast.Data;
using Teamcast.DTOs;
using Teamcast.Models;

namespace Teamcast.Repos
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dbContext;
        private readonly IConfiguration _config;
        private int Iterations { get; set; } = 10000;
        private const int SaltSize = 16; // 128 bit 
        private const int KeySize = 32; // 256 bit

        public AuthRepository(DataContext userDb, IConfiguration config, IMapper mapper)
        {
            _dbContext = userDb;
            _config = config;
        }

        public async Task<User> Login(string username, string password)
        {
            //Retrieve the user that matches the given user name and password
            var repoUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower()); ;

            //If this user does not exist, return NULL
            if (repoUser == null || !Check(repoUser.Password, password))
                return null;

            repoUser.LastActive = DateTime.Now;

            _dbContext.Users.Update(repoUser);
            await SaveChanges();

            repoUser.Token = GenerateToken(repoUser);

            return repoUser;
        }

        public async Task<User> Register(User user)
        {
            user.Password = Hash(user.Password);

            await _dbContext.Users.AddAsync(user);
            await SaveChanges();

            return user;
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        //Secure password implemetion source:
        //https://medium.com/dealeron-dev/storing-passwords-in-net-core-3de29a3da4d2
        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              SaltSize,
              Iterations,
              HashAlgorithmName.SHA256))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{Iterations}.{salt}.{key}";
            }
        }

        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              iterations,
              HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                return verified;
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (!await _dbContext.Users.AnyAsync(x => x.Username == username))
                return false;

            return true;
        }

        public async Task<bool> DeleteUser(User user)
        {
            _dbContext.Users.Remove(user);

            var _event = _dbContext.Events.Where(e => e.User.Id == user.Id).ToList();
            var _team = _dbContext.Teams.Where(t => t.UserId == user.Id).ToList();

            if (_event.Count() > 0)
            {
                foreach (var ev in _event)
                {
                    _dbContext.Remove(ev);
                }
            }

            if (_team.Count() > 0)
            {
                foreach (var t in _team)
                {
                    _dbContext.Remove(t);

                    var _teamMem = _dbContext.TeamMembers
                        .Where(tm => tm.TeamId == t.Id).ToList();

                    if (_teamMem.Count > 0)
                    {
                        foreach (var tm in _teamMem)
                            _dbContext.Remove(tm);
                    }
                }
            }

            return await SaveChanges();
        }

        public async Task<bool> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
        }

        public string GenerateToken(User user)
        {
            //If the user was found, generate and set up an Authentication Token that user
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSetting:Secret").Value);

            //Set token description
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                //Token expiration
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                , SecurityAlgorithms.HmacSha256Signature)
            };
            //Create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenToReturn = tokenHandler.WriteToken(token);

            return tokenToReturn;
        }
    }
}
