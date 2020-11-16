using System;
using System.Collections.Generic;
using System.Text;

namespace HealthStore.Models.Contracts.Requests
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
