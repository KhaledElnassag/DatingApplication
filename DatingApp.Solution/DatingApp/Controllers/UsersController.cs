using AutoMapper;
using DatingApp.Core.Dtos;
using DatingApp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
	[Authorize]
	public class UsersController : BaseApiController
	{
		private readonly UserManager<ApplicationUser> _UserManager;
		private readonly IMapper _Mapper;

		public UsersController(UserManager<ApplicationUser> userManager,IMapper mapper)
		{
			_UserManager = userManager;
			_Mapper = mapper;
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<MembrsDto>> GetUser(string id)
		{
			var User= await _UserManager.Users.Include(U => U.Photos).FirstOrDefaultAsync(U=>U.Id==id);
			return Ok(_Mapper.Map<MembrsDto>(User));
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MembrsDto>>> GetUsers()
		{
			var Users= await _UserManager.Users.Include(U=>U.Photos).ToListAsync();
			return Ok(_Mapper.Map<IEnumerable<MembrsDto>>( Users));
		}
	}
}
