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
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            if (request == null) return NotFound();
            var user = _mapper.Map<User>(request);
            var result = await _userService.Create(user);
            if (result == null) return NotFound();
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            if (result == null) return NotFound("Collection is empty!");
            var response = _mapper.Map<IEnumerable<UserResponse>>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0) return BadRequest();

            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound();
            await _userService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserRequest request)
        {
            if (request == null) return NotFound();
            var user = _mapper.Map<User>(request);
            var result = await _userService.Update(user);
            if (result == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var result = await _userService.GetUserById(userId);
            if (result == null) return NotFound("Specialty not found");

            var specialty = _mapper.Map<User>(result);

            return Ok(specialty);
        }
    }
}