using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Teamcast.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("CreatorUserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public Point Location { get; set; }
        [Required]
        public int CategoryType { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }
        [Required]
        public int MinMembers { get; set; }
        [Required]
        public int MaxMembers { get; set; }
        [Required]
        public int CompensationType { get; set; }
        public float MoneyCompensationAmount { get; set; }
        public string OtherCompensationDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<EventMember> EventMember { get; set; }
        public Event()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
