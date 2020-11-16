using System;
using System.Collections.Generic;
using System.Text;

namespace HealthStore.Models.Contracts.Responses
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public DateTime StartAt { get; set; }
    }
}
