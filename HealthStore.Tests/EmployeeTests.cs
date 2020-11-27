using AutoMapper;
using HealthStore.BL.Interfaces.Users;
using HealthStore.BL.Services.Users;
using HealthStore.Controllers.Users;
using HealthStore.DL.Interfaces.Users;
using HealthStore.Extensions;
using HealthStore.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HealthStore.Tests
{
    public class EmployeeTests
    {
        private IMapper _mapper;
        private Mock<IEmployeeRepository> _employeeRepository;
        private IEmployeeService _employeeService;
        private EmployeeController _controller;
        IList<Employee> _employees = new List<Employee>()
        {
            {new Employee { Id = 1, Name = "test emp name", Salary = 1000, StartAt = new DateTime() } }
        };
        public EmployeeTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_employeeRepository.Object);
            _controller = new EmployeeController(_employeeService, _mapper);
        }

        [Fact]
        public async Task Employee_GetById_NotFound()
        {
            var employeeId = 3;

            _employeeRepository.Setup(x => x.GetById(employeeId))
                .ReturnsAsync(_employees.FirstOrDefault(x => x.Id == employeeId));

            var result = await _controller.GetEmployeeById(employeeId);

            var notFoundObjectResult = result as NotFoundObjectResult;

            Assert.NotNull(notFoundObjectResult);
        }
    }
}
