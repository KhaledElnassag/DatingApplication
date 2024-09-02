using DatingApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Repository.DataBase
{
	public static class SeedingData
	{
		public static async Task Seeding(ApplicationContext _Context,UserManager<ApplicationUser>_UserManager)
		{
			if (!_Context.Users.Any())
			{
				var ApplicationUser = new ApplicationUser
				{
					UserName= "khalidelnassag",
					Email ="khalidelnassag@gmail.com",
				};
				await _UserManager.CreateAsync(ApplicationUser, "Pa$$w0rd");
			}
		}
	}
}
