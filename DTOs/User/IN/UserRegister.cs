using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Teamcast.DTOs
{
    public class UserRegister
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lastname { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsActiveUser { get; set; }

        public UserRegister()
        {
            DateJoined = DateTime.Now;
            IsActiveUser = true;
        }

    }
}
