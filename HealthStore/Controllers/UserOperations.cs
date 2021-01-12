using AutoMapper;
using HealthStore.BL.Interfaces.Users;
using HealthStore.Models.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthStore.Controllers
{
    public class UserOperations : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserOperations(IMapper mapper, IEmployeeService employeeService, IUserService userService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
            _userService = userService;
        }

        [HttpPost("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, double salary)
        {
            var result = await _employeeService.UpdateEmployeeSalary(employeeId, salary);

            if (result == null) return NotFound();

            var employee = _mapper.Map<EmployeeResponse>(result);
            return Ok(employee);
        }

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int userId, string name)
        {
            var result = await _userService.UpdateUserName(userId, name);

            if (result == null) return NotFound();

            var user = _mapper.Map<UserResponse>(result);
            return Ok(user);
        }
    }
}
