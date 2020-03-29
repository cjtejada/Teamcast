using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Teamcast.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public string Role { get; set; }
        public DateTime DateJoined { get; set; }
        public User User { get; set; }
        public TeamMember()
        {
            DateJoined = DateTime.Now;
        }
    }
}
