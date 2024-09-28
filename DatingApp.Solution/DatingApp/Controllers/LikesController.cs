using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.Core.Dtos;
using DatingApp.Core.Interfaces;
using DatingApp.Core.Models;
using DatingApp.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DatingApp.Controllers
{
	[Authorize]
	public class LikesController : BaseApiController
	{
		private readonly UserManager<ApplicationUser> _UserManager;
		private readonly IUnitOfWork _UnitOfWork;
		private readonly IMapper _Mapper;

		public LikesController(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork,IMapper mapper)
		{
			_UserManager = userManager;
			_UnitOfWork = unitOfWork;
			_Mapper = mapper;
		}
		[HttpGet("{userName}")]
		public async Task<ActionResult<MembrsDto>> GetUser(string userName)
		{
			var name = User.FindFirstValue(ClaimTypes.Name);
			var cUser= await _UserManager.Users.FirstOrDefaultAsync(U=>U.UserName.ToLower()== name.ToLower());
			var likedUser= await _UserManager.Users.FirstOrDefaultAsync(U=>U.UserName.ToLower()== userName.ToLower());
			cUser.YourLikes.Add(new UserLike
			{
				LikeById = likedUser.Id
			});
			await _UserManager.UpdateAsync(cUser);
			return Ok();
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MembrsDto>>> GetUsers()
		{
			var name = User.FindFirstValue(ClaimTypes.Name);
			var likedUsers = await _UserManager.Users.
				Where(U => U.UserName.ToLower() == name.ToLower()).SelectMany(U=>U.YourLikes).Include(UL=>UL.LikeBy).ThenInclude(L=>L.Photos).Select(L=>L.LikeBy)
				.ProjectTo<MembrsDto>(_Mapper.ConfigurationProvider).ToListAsync();		
			return Ok(likedUsers);
		}


	}
}
