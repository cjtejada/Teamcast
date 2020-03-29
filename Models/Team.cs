using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Teamcast.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Column("CreatorUserId")]
        public int UserId { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public Uri TeamPhoto { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public User User { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }
        public Team()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
