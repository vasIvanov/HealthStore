using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthStore.BL.Interfaces.Users;
using HealthStore.Models.Contracts.Requests;
using HealthStore.Models.Contracts.Responses;
using HealthStore.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthStore.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private IMapper _mapper;
        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequest request)
        {
            if (request == null) return NotFound();
            var employee = _mapper.Map<Employee>(request);
            var result = await _employeeService.Create(employee);
            if (result == null) return NotFound();
            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _employeeService.GetAll();
            if (result == null) return NotFound("Collection is empty!");
            var response = _mapper.Map<IEnumerable<EmployeeResponse>>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id <= 0) return BadRequest();

            var employee = await _employeeService.GetUserById(id);
            if (employee == null) return NotFound();
            await _employeeService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeRequest request)
        {
            if (request == null) return NotFound();
            var employee = _mapper.Map<Employee>(request);
            var result = await _employeeService.Update(employee);
            if (result == null) return NotFound();
            return Ok(employee);
        }
    }
}
