﻿using HealthStore.BL.Interfaces;
using HealthStore.Models.Contracts.Requests;
using HealthStore.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

		IIdentityService _identityService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public IdentityController(IIdentityService identityService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_identityService = identityService;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
		{
			var existingUser = _userManager.FindByNameAsync(request.UserName);

			if (existingUser != null)
			{
				return BadRequest("User already exist");
			}

			var user = new ApplicationUser
			{
				UserName = request.UserName,
				Email = request.UserName
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			return Ok(result);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
		{
			var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> LogOff()
		{
			await _signInManager.SignOutAsync();

			return Ok();
		}

		[HttpGet("GetCurrentUser")]
		public async Task<IActionResult> GetCurrentUser()
		{
			var result = await _userManager.GetUserAsync(HttpContext.User);

			return Ok(result);
		}
	}
}
