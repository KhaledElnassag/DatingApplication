using DatingApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DatingApp.Repository.DataBase
{
	public static class SeedingData
	{
		public static async Task Seeding(ApplicationContext _Context,UserManager<ApplicationUser>_UserManager)
		{
			if (_Context.Users.Any())
			{
				var json = File.ReadAllText("../DatingApp.Repository/DataBase/SeedingData/UserSeedData.json");
				var users =JsonSerializer.Deserialize<List<ApplicationUser>>(json);
				if (users != null && users.Count() > 0)
				{
                    foreach (ApplicationUser item in users)
                    {
						item.UserName = item?.UserName?.ToLower();
						item.Email = $"{item?.UserName?.ToLower()}@gmail.com";
						await _UserManager.CreateAsync(item, "Pa$$w0rd");
					}
                }
			}
			if (!_Context.Users.Any(U=>U.UserName== "khalidelnassag"))
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
