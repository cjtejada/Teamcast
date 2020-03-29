using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teamcast.DTOs.TeamMember.OUT
{
    public class TeamMemberDto
    {
        public string Role { get; set; }
        public DateTime DateJoined { get; set; }
        public UserDto User { get; set; }
    }
}
