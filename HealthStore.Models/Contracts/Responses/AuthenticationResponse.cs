using System;
using System.Collections.Generic;
using System.Text;

namespace HealthStore.Models.Contracts.Responses
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
