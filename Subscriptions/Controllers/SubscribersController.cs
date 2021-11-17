using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Subscribtions.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Subscribtions.Controllers
{
    /// <summary>
    /// Deals with Telecom company subscribers
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SubscribersController : ControllerBase
    {

        public SubscribersController()
        {
        }

        /// <summary>
        /// Get basic information about subscribers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<SubscriberBase> GetSubscribersBasicInfo()
        {
            var subscribers = new List<SubscriberBase>
            {
                new SubscriberBase
                {
                    FirstName = "Rema",
                    LastName = "Ahamd",
                    PhoneNumber = "0599323450",
                    SubscriptionDate = DateTime.Now,
                    Program = Enums.ProgramType.Prepaid
                },
                new SubscriberBase
                {
                    FirstName = "Sameer",
                    LastName = "Tawfiq",
                    PhoneNumber = "05949876543",
                    SubscriptionDate = DateTime.Now,
                    Program = Enums.ProgramType.Bill
                }
            };

            return subscribers;
        }

        /// <summary>
        /// Get subscriber detailed info
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public string GetSubscriberDetailedaInfo(int id)
        {
            return "subscriber detailed info";
        }
    }
}
