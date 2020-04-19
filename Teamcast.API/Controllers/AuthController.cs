using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teamcast.DTOs;
using Teamcast.Models;
using Teamcast.Repos;

namespace Teamcast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _userRepo;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult>Login([FromBody]Login _user)
        {
            var user = await _userRepo.Login(_user.Username, _user.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect." });

            var repoUser = _mapper.Map<UserDto>(user);

            return Ok(new { repoUser, Token = user.Token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserRegister userToCreate)
        {
            var _user = _mapper.Map<User>(userToCreate);

            if (await _userRepo.UserExists(_user.Username))
                return BadRequest(new { message = "Username already exists" });

            var user = await _userRepo.Register(_user);

            if (user == null)
                return BadRequest(new { message = "Error while registering..." });

            var userToReturn = _mapper.Map<UserDto>(user);

            var loginCreds = new Login()
            {
                Username = userToCreate.Username,
                Password = userToCreate.Password
            };

            return await Login(loginCreds);
        }

        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int userId, [FromBody]UserUpdate user)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            var _user = await _userRepo.GetUser(userId);

            _mapper.Map(user,_user);

            if (!await _userRepo.SaveChanges())
                return BadRequest(new { message = $"Something when wrong while updating User {userId}..." });

            return Ok(user);
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(int userId, string password)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            var user = await _userRepo.GetUser(userId);

            user.Password = _userRepo.Hash(password);

            if (!await _userRepo.SaveChanges())
                return BadRequest(new { message = $"Something when wrong while updating User {userId}..." });

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            var user = await _userRepo.GetUser(userId);

            if (!await _userRepo.DeleteUser(user))
                return BadRequest(new { message = $"Something went wrong when deleting {userId}" });

            return Ok();
        }
    }
}