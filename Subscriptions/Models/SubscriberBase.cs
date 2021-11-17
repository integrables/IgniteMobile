using Subscribtions.Enums;
using System;

namespace Subscribtions.Models
{
    public class SubscriberBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public ProgramType Program { get; set; }
    }
}
