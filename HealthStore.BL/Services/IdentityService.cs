﻿using HealthStore.BL.Interfaces;
using HealthStore.Models.Common;
using HealthStore.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.BL.Services
{
    public class IdentityService : IIdentityService
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly JwtSettings _jwtSettings;
		public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtSettings jwtSettings)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtSettings = jwtSettings;
		}

		public async Task<AuthenticationResult> LoginAsync(string userName, string password)
		{
			var user = await _userManager.FindByNameAsync(userName);

			if (user == null)
			{
				return new AuthenticationResult
				{
					Errors = new string[] { $"User/Password combination is wrong!" },
					IsSuccess = false
				};
			}

			var validPassword = await _userManager.CheckPasswordAsync(user, password);

			if (!validPassword)
			{
				return new AuthenticationResult
				{
					Errors = new string[] { $"User/Password combination is wrong!" },
					IsSuccess = false
				};
			}

			return await GenerateAuthenticationResult(user);
		}

		public async Task<AuthenticationResult> RegisterAsync(string userName, string password)
		{
			var existingUser = await _userManager.FindByNameAsync(userName);

			if (existingUser != null)
			{
				return new AuthenticationResult
				{
					Errors = new string[] { $"User {userName} already exist!" }
				};
			}

			var user = new ApplicationUser
			{
				UserName = userName,
				Email = userName,
				BirthDay = new DateTime()
			};

			var result = await _userManager.CreateAsync(user, password);

			if (!result.Succeeded)
			{
				return new AuthenticationResult
				{
					Errors = result.Errors.Select(x => x.Description)
				};
			}

			return await GenerateAuthenticationResult(user);

		}

		private async Task<AuthenticationResult> GenerateAuthenticationResult(ApplicationUser user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new System.Security.Claims.ClaimsIdentity(new[]
				{
					new Claim(JwtRegisteredClaimNames.Sub, user.Email),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim(JwtRegisteredClaimNames.Email, user.Email),
					new Claim("id", user.Id.ToString()),
					new Claim("View", "View")
				}),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			await _signInManager.SignInAsync(user, false);

			return new AuthenticationResult
			{
				IsSuccess = true,
				Token = tokenHandler.WriteToken(token)
			};
		}
	}
}
