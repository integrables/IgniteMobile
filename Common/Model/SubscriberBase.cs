using Common.Enums;
using System;

namespace Common.Models
{
    public class SubscriberBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public ProgramType Program { get; set; }
    }
}
