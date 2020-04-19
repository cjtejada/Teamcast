using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teamcast.DTOs.Team.IN;
using Teamcast.DTOs.Team.OUT;
using Teamcast.Models;
using Teamcast.Repos;

namespace Teamcast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository _tRepo;
        private readonly IMapper _mapper;
        public TeamsController(ITeamRepository tRepo, IMapper mapper)
        {
            _tRepo = tRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            var teamObjList = _mapper
                .Map<List<TeamDto>>(await _tRepo.GetTeams());

            if (teamObjList == null)
                return NotFound(new { message = "Something went wrong..." });

            return Ok(teamObjList);
        }

        [HttpGet("{teamId:int}")]
        public async Task<IActionResult> GetTeam(int teamId)
        {
            var teamObj = await _tRepo.GetTeam(teamId);

            if (teamObj == null)
                return NotFound();

            var team = _mapper.Map<TeamDto>(teamObj);

            return Ok(team);
        }

        [Authorize]
        [HttpPost("CreatTeam")]
        public async Task<IActionResult> CreateTeam(int userId, [FromBody] TeamCreate teamcr)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            if (teamcr == null)
                return BadRequest(ModelState);

            var repoTeam = _mapper.Map<Team>(teamcr);

            repoTeam.UserId = userId;

            if (!await _tRepo.CreateTeam(userId, repoTeam))
                return StatusCode(500, ModelState);

            return Ok(repoTeam);
        }

        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int userId, [FromBody]TeamUpdate team)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            var _team = await _tRepo.GetTeam(team.Id);

            if (_team.UserId != userId)
                return Unauthorized();

            _mapper.Map(team, _team);

            if (!await _tRepo.SaveChanges())
                return BadRequest(new { message = $"Something when wrong while updating Event {team.Id}..." });

            return Ok(team);
        }

        [Authorize]
        [HttpPost("JoinTeam")]
        public async Task<IActionResult> JoinTeam(int userId, int teamId)
        {
            if (userId == 0 || teamId == 0)
                return BadRequest();

            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            if (!await _tRepo.UserIdExists(userId))
                return Unauthorized(new { message = "User does not exist." });

            if (!await _tRepo.TeamExists(teamId))
                return BadRequest(new { message = "Event does not exist" });

            if (await _tRepo.IsTeamMember(userId, teamId))
                return Unauthorized(new { message = "You're already part of this group" });

            if (await _tRepo.IsTeamOwner(userId))
                return Unauthorized(new { message = "You're already the owner of this team." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var member = new TeamMember()
            {
                UserId = userId,
                TeamId = teamId
            };

            if (!await _tRepo.JoinTeam(member))
                return StatusCode(500, ModelState);

            return Ok(member);
        }

        [Authorize]
        [HttpPost("JoinEvent")]
        public async Task<IActionResult> JoinEvent(int userId, int teamId, int eventId)
        {
            if (userId == 0 || teamId == 0)
                return BadRequest();

            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            if (!await _tRepo.IsTeamOwner(userId))
                return Unauthorized(new { message = "You're not the owner of this team." });

            if (!await _tRepo.TeamExists(teamId))
                return BadRequest(new { message = "Team does not exist" });

            if (await _tRepo.IsTeamMember(userId, teamId))
                return Unauthorized(new { message = "You're already part of this Team" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var member = new EventMember()
            {
                UserId = userId,
                EventId = eventId,
                TeamId = teamId
            };

            if (!await _tRepo.JoinEvent(member))
                return StatusCode(500, ModelState);

            return Ok(member);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int userId, int teamId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            var team = await _tRepo.GetTeam(teamId);

            if (team == null)
                return NotFound(new { message = "Event does not exist" });

            if (team.UserId != userId)
                return Unauthorized();

            if (!await _tRepo.DeleteTeam(team))
                return BadRequest(new { message = $"Something went wrong when deleting {teamId}" });

            return Ok();
        }
    }
}