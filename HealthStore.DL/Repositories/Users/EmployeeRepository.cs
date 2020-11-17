using HealthStore.DL.Interfaces.Users;
using HealthStore.Models.Common;
using HealthStore.Models.Users;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.DL.Repositories.Users
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employees;
        public EmployeeRepository(IOptions<MongoDbConfiguration> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            var database = client.GetDatabase(config.Value.DatabaseName);

            _employees = database.GetCollection<Employee>("Employee");
        }
        public async Task<Employee> Create(Employee employee)
        {
            await _employees.InsertOneAsync(employee);
            return employee;
        }

        public async Task Delete(int employeeId)
        {
            await _employees.DeleteOneAsync(p => p.Id == employeeId);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var result = await _employees.FindAsync(p => true);
            return result.ToEnumerable();
        }

        public async Task<Employee> GetById(int employeeId)
        {
            var result = await _employees.FindAsync(p => p.Id == employeeId);

            return result.FirstOrDefault();
        }

        public async Task<Employee> GetByName(string name)
        {
            var result = await _employees.FindAsync(p => p.Name == name);

            return result.FirstOrDefault();
        }

        public async Task<Employee> Update(Employee employee)
        {
            await _employees.ReplaceOneAsync(p => p.Id == employee.Id, employee);
            return employee;
        }
    }
}
