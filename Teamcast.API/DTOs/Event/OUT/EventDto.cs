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
        //public string Location { get; set; }
        public int CategoryType { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int MinUsersWanted { get; set; }
        public int MaxUsersWanted { get; set; }
        public int CompensationType { get; set; }
        public float MoneyCompensationAmount { get; set; }
        public string OtherCompensationDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserDto EventOwner { get; set; }
        public ICollection<EventMemberDto> EventMember { get; set; }
    }
}
