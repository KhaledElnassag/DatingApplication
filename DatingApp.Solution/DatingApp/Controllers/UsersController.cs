using DatingApp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
	
	public class UsersController : BaseApiController
	{
		private readonly UserManager<ApplicationUser> _UserManager;

		public UsersController(UserManager<ApplicationUser> userManager)
		{
			_UserManager = userManager;
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<ApplicationUser>> GetUser(string id)
		{
			var User= await _UserManager.Users.FirstOrDefaultAsync(U=>U.Id==id);
			return Ok(User);
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
		{
			var Users= await _UserManager.Users.ToListAsync();
			return Ok(Users);
		}
	}
}
