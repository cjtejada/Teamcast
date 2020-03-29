using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teamcast.DTOs;
using Teamcast.Repos;

namespace Teamcast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _uRepo;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository uRepo, IMapper mapper)
        {
            _uRepo = uRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(int? pageNumber, int? pageSize, string userSearch)
        {
            var _pageNumber = pageNumber ?? 1;
            var _pageSize = pageSize ?? 10;

            var users = await _uRepo.GetUsers(userSearch);

            var teamObjList = _mapper.Map<List<UserDto>>
                (users.Skip((_pageNumber - 1) * _pageSize).Take(_pageSize));

            if (teamObjList == null)
                return NotFound(new { message = "Nothing found..." });

            return Ok(teamObjList);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var teamObj = await _uRepo.GetUser(userId);

            if (teamObj == null)
                return NotFound(new { message = "User not found..." });

            var team = _mapper.Map<UserDto>(teamObj);

            return Ok(team);
        }
    }
}