using DatingApp.Core.Interfaces;
using DatingApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Service
{
	public class TokenService : ITokenService
	{
		private readonly UserManager<ApplicationUser> _UserManager;
		private readonly IConfiguration _Configuration;

		public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
		{
			_UserManager = userManager;
			_Configuration = configuration;
		}
		public async Task<string> GenerateTokenAsync(ApplicationUser user)
		{
			if (user is not null)
			{
				var Claims = new List<Claim>
				{
				new Claim(ClaimTypes.Name,user.UserName)
				//new Claim(ClaimTypes.Email,user.Email)
				};
				var Roles = (await _UserManager.GetRolesAsync(user));
				if (Roles != null && Roles.Count > 0)
				{
					var RoleClaims = Roles.Select(C => new Claim(ClaimTypes.Role, C)).ToList();
					Claims.AddRange(RoleClaims);
				}
				var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:Key"]));
				var Jwt = new JwtSecurityToken
					(
					issuer: _Configuration["JWT:ValidIssuer"],
					audience: _Configuration["JWT:ValidAudience"],
					expires: DateTime.Now.AddDays(double.Parse(_Configuration["JWT:DurationInDays"])),
					signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature),
					claims: Claims
					);
				return new JwtSecurityTokenHandler().WriteToken(Jwt);
			}
			return null;
		}


	}
}
