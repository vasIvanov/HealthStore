﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HealthStore.Models.Contracts.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
