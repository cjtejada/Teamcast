using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teamcast.DTOs.Team.IN
{
    public class TeamCreate
    {
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public Uri TeamPhoto { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
