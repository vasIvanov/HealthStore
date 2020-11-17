using HealthStore.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.DL.Interfaces.Users
{
    public interface IEmployeeRepository
    {
        Task<Employee> Create(Employee employee);
        Task Delete(int employeeId);
        Task<Employee> Update(Employee employee);
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetById(int employeeId);
        Task<Employee> GetByName(string name);
    }
}
