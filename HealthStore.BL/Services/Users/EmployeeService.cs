using HealthStore.BL.Interfaces.Users;
using HealthStore.DL.Interfaces.Users;
using HealthStore.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.BL.Services.Users
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Employee> Create(Employee employee)
        {
            List<Task> tasks = new List<Task>();
            var uniqueId = _employeeRepository.GetById(employee.Id);
            tasks.Add(uniqueId);
            var uniqueName = _employeeRepository.GetByName(employee.Name);
            tasks.Add(uniqueName);

            await Task.WhenAll(tasks);
            if (uniqueId.Result == null && uniqueName.Result == null)
            {
                return await _employeeRepository.Create(employee);
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task Delete(int id)
        {
            await _employeeRepository.Delete(id);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _employeeRepository.GetAll();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetById(id);
        }

        public async Task<Employee> GetUserByName(string name)
        {
            return await _employeeRepository.GetByName(name);
        }

        public async Task<Employee> Update(Employee employee)
        {
            List<Task> tasks = new List<Task>();
            var idExists = _employeeRepository.GetById(employee.Id);
            tasks.Add(idExists);
            var uniqueName = _employeeRepository.GetByName(employee.Name);
            tasks.Add(uniqueName);

            await Task.WhenAll(tasks);
            if (idExists.Result != null)
            {
                if(uniqueName.Result.Id == employee.Id || uniqueName.Result == null)
                {
                    return await _employeeRepository.Update(employee);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<Employee> UpdateEmployeeSalary(int employeeId, double salary)
        {
            var employee = await _employeeRepository.GetById(employeeId);
            if(employee != null)
            {
                employee.Salary = salary;
            }

            var result = await _employeeRepository.Update(employee);
            return result;
        }
    }
}
