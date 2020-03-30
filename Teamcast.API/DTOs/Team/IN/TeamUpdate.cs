using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teamcast.DTOs.Team.IN
{
    public class TeamUpdate
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
    }
}
