using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teamcast.DTOs
{
    public class EventUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryType { get; set; }
        public int MinMembers { get; set; }
        public int MaxMembers { get; set; }
        public int CompensationType { get; set; }
        public float MoneyCompensationAmount { get; set; }
        public string OtherCompensationDescription { get; set; }
    }
}
