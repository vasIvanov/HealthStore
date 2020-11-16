using HealthStore.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthStore.Models.Products
{
    public class Diet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DietGoal { get; set; }
        public int Days { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public int SuitablePlanId { get; set; }
        public double Price { get; set; }
    }
}
