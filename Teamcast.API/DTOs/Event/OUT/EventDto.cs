using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teamcast.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public sbyte CategoryType { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public sbyte MaxMembers { get; set; }
        public sbyte CompensationType { get; set; }
        public float MoneyCompensationAmount { get; set; }
        public sbyte MoneyCompensationRate { get; set; }
        public string OtherCompensationDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserDto EventOwner { get; set; }
        public ICollection<EventMemberDto> EventMember { get; set; }
    }
}
