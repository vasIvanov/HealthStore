using System;
using System.Collections.Generic;
using System.Text;

namespace HealthStore.Models.Contracts.Requests
{
    public class EmployeeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public DateTime StartAt { get; set; }
    }
}
