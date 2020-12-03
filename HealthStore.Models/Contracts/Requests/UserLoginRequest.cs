using System;
using System.Collections.Generic;
using System.Text;

namespace HealthStore.Models.Contracts.Requests
{
    public class UserLoginRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
