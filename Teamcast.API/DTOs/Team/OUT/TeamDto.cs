using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamcast.DTOs.TeamMember.OUT;
using Teamcast.Models;

namespace Teamcast.DTOs.Team.OUT
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public Uri TeamPhoto { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserDto User { get; set; }
        public ICollection<TeamMemberDto> TeamMember { get; set; }
    }
}
