using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamcast.DTOs.Team.OUT;
using Teamcast.DTOs.TeamMember.OUT;
using Teamcast.Models;

namespace Teamcast.DTOs
{
    public class EventMemberDto
    {
        public string Role { get; set; }
        public DateTime DateJoined { get; set; }
        public UserDto User { get; set; }
        public TeamDto Teams { get; set; }
    }
}
