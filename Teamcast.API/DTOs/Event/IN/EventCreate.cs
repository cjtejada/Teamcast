using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Teamcast.Models;

namespace Teamcast.DTOs
{
    public class EventCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public sbyte CategoryType { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }
        [Required]
        public sbyte MaxMembers { get; set; }
        [Required]
        public sbyte CompensationType { get; set; }
        public float MoneyCompensationAmount { get; set; }
        public sbyte MoneyCompensationRate { get; set; }
        public string OtherCompensationDescription { get; set; }
    }
}
