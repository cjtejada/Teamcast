using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Teamcast.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Column("PasswordHash")]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime LastActive { get; set; }
        public Uri UserPhoto { get; set; }
        public ICollection<Event> Events { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
