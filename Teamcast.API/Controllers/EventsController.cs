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
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _evRepo;
        private readonly IMapper _mapper;
        public EventsController(IEventRepository evRepo, IMapper mapper)
        {
            _evRepo = evRepo;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEvents(int? userId, int? pageNumber, int? pageSize, string sort, string search, double lat, double lon, double? radius)
        {
            var _pageNumber = pageNumber ?? 1;
            var _pageSize = pageSize ?? 5;
            var _userId = userId ?? 0;
            var _radius = radius ?? 40234;

            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                _userId = 0;

            List<Event> eventsObj;

            eventsObj = await _evRepo.GetEvents(_userId, sort, search, lat, lon, _radius);

            var eventObjList = _mapper.Map<List<EventDto>>
                (eventsObj.Skip((_pageNumber - 1) * _pageSize).Take(_pageSize));

            if (eventObjList == null)
                return NotFound(new { message = "Something went wrong..." });

            return Ok(eventObjList);
        }

        [Authorize]
        [HttpGet("{eventId:int}")]
        public async Task<IActionResult> GetEvent(int userId, int eventId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            var evObj = await _evRepo.GetEvent(eventId);

            if (evObj == null)
                return NotFound();

            var eventObj = _mapper.Map<EventDto>(evObj);

            return Ok(eventObj);
        }

        [Authorize]
        [HttpPost("CreatEvent")]
        public async Task<IActionResult> CreateEvent(int userId, [FromBody] EventCreate ev)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            if (ev == null)
                return BadRequest(ModelState);

            var repoEvent = _mapper.Map<Event>(ev);

            if (!await _evRepo.CreateEvent(userId, repoEvent))
                return StatusCode(500, ModelState);

            return Ok(ev);
        }

        [Authorize]
        [HttpPost("JoinEvent")]
        public async Task<IActionResult> JoinEvent(int userId, int eventId)
        {
            if (userId == 0 || eventId == 0)
                return BadRequest();

            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            if (!await _evRepo.UserIdExists(userId))
                return Unauthorized(new { message = "User does not exist." });

            if (!await _evRepo.EventExists(eventId))
                return BadRequest(new { message = "Event does not exist" });

            if (await _evRepo.IsEventMember(userId, eventId))
                return Unauthorized(new { message = "You're already part of this Event" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var member = new EventMember()
            {
                UserId = userId,
                EventId = eventId
            };

            if (!await _evRepo.JoinEvent(member))
                return StatusCode(500, ModelState);

            return Ok(member);
        }

        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int userId, [FromBody]EventUpdate ev)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            var _event = await _evRepo.GetEvent(ev.Id);

            if (_event.User.Id != userId)
                return Unauthorized();

            _mapper.Map(ev, _event);

            if (!await _evRepo.SaveChanges())
                return BadRequest(new { message = $"Something when wrong while updating Event {ev.Id}..." });

            return Ok(ev);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int userId, int eventId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.Name)?.Value))
                return Unauthorized();

            var _event = await _evRepo.GetEvent(eventId);

            if (_event == null)
                return NotFound(new { message = "Event does not exist" });

            if (_event.User.Id != userId)
                return Unauthorized();

            if (!await _evRepo.DeleteEvent(_event))
                return BadRequest(new { message = $"Something went wrong when deleting {eventId}" });

            return Ok();
        }
    }
}