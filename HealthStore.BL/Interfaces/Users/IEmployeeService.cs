using HealthStore.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.BL.Interfaces.Users
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> GetUserByName(string name);
        Task<Employee> Create(Employee employee);
        Task<IEnumerable<Employee>> GetAll();
        Task Delete(int id);
        Task<Employee> Update(Employee employee);
    }
}
