using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Teamcast.Models
{
    public class EventMember
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int EventId { get; set; }
        public int? TeamId { get; set; }
        public DateTime DateJoined { get; set; }
        public string Role { get; set; }
        public string RoleDescription { get; set; }
        public User User { get; set; }
        public Team Team { get; set; }
        public EventMember()
        {
            DateJoined = DateTime.Now;
        }
    }
}
