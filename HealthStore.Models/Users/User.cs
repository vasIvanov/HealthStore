using HealthStore.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthStore.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
