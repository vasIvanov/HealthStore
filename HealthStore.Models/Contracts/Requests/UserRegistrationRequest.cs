﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HealthStore.Models.Contracts.Requests
{
    public class UserRegistrationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
