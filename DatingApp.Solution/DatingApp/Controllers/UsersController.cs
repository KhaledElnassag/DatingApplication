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
	public class UsersController : BaseApiController
	{
		private readonly UserManager<ApplicationUser> _UserManager;
		private readonly IUnitOfWork _UnitOfWork;
		private readonly IMapper _Mapper;
		private readonly IPhotoService _photoService;

		public UsersController(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork,IMapper mapper,
			IPhotoService photoService)
		{
			_UserManager = userManager;
			_UnitOfWork = unitOfWork;
			_Mapper = mapper;
			_photoService = photoService;
		}
		[HttpGet("{userName}")]
		public async Task<ActionResult<MembrsDto>> GetUser(string userName)
		{
			var User= await _UserManager.Users.Include(U => U.Photos).FirstOrDefaultAsync(U=>U.UserName.ToLower()== userName.ToLower());
			return Ok(_Mapper.Map<MembrsDto>(User));
		}
		[HttpPost]
		public async Task<ActionResult<Pagination<MembrsDto>>> GetUsers(UserParams userparams)
		{
			var maxAge = DateOnly.FromDateTime(DateTime.Today.AddYears(-userparams.MaxAge));
			var minAge = DateOnly.FromDateTime(DateTime.Today.AddYears(-userparams.MinAge));
			var query= _UserManager.Users.Include(U=>U.Photos)
				.Where(U => U.DateOfBirth <= minAge && U.DateOfBirth >= maxAge
				&& (string.IsNullOrEmpty(userparams.Gender)||userparams.Gender==U.Gender))
				.Skip((userparams.PageIndex-1)*userparams.PageSize)
				.Take(userparams.PageSize).ProjectTo<MembrsDto>(_Mapper.ConfigurationProvider);
			var Users= await query.ToListAsync();
			var count =await _UserManager.Users
				.Where(U => U.DateOfBirth <= minAge && U.DateOfBirth >= maxAge
				&& (string.IsNullOrEmpty(userparams.Gender) || userparams.Gender == U.Gender)).CountAsync();
			var data = new Pagination<MembrsDto>(count, userparams.PageIndex, userparams.PageSize, Users);
			return Ok(data);
		}

		[HttpPut("update")]
		public async Task<ActionResult<IEnumerable<MembrsDto>>> Update(UpdateDto updateDto)
		{
			var curUserName = User.FindFirstValue(ClaimTypes.Name);
			var user = await _UserManager.Users.Include(U => U.Photos).FirstOrDefaultAsync(U => U.UserName.ToLower() == curUserName.ToLower());
			_Mapper.Map(updateDto,user );
			var succeed=await _UnitOfWork.CompleteAsync();
			if(succeed>0)
			return Ok();
			return BadRequest(new ErrorDto(400))
				;		}
		[HttpPost("add-photo")]
		public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile File)
		{
			var usernam = User.FindFirstValue(ClaimTypes.Name);
			var appuser = await _UserManager.Users.Include(U => U.Photos).FirstOrDefaultAsync(U => U.UserName == usernam.ToLower());
			if (appuser is null) return NotFound(new ErrorDto(404));
			var result = await _photoService.AddPhotoAsync(File);
			if (result.Error != null) return BadRequest(new ErrorDto(400));
			var photo = new Photo()
			{
				PublicId = result.PublicId,
				Url = result.SecureUrl.AbsoluteUri
			};
			if (appuser.Photos.Count() == 0) photo.IsMain = true;
			appuser.Photos.Add(photo);
			var res = await _UserManager.UpdateAsync(appuser);
			if (!res.Succeeded) return BadRequest(new ErrorDto(400));

			return CreatedAtAction(nameof(GetUser), new { username = appuser.UserName }, _Mapper.Map<PhotoDto>(photo));
		}
		[HttpPut("set-main-photo/{photoId}")]
		public async Task<ActionResult> SetMainhoto(int photoId)
		{
			var usernam = User.FindFirstValue(ClaimTypes.Name);
			var appuser = await _UserManager.Users.Include(U => U.Photos).FirstOrDefaultAsync(U => U.UserName == usernam.ToLower());
			if (appuser is null) return NotFound(new ErrorDto(404));
			var photo = appuser.Photos.FirstOrDefault(P => P.Id == photoId);
			if (photo == null) return NotFound(new ErrorDto(404));
			if (!photo.IsMain)
			{
				var MainPhoto = appuser.Photos.FirstOrDefault(P => P.IsMain);
				if (MainPhoto == null) return NotFound(new ErrorDto(404));
				MainPhoto.IsMain = false;
				photo.IsMain = true;
				var res = await _UserManager.UpdateAsync(appuser);
				if (!res.Succeeded) return BadRequest(new ErrorDto(400));
			}
			return NoContent();
		}
		[HttpDelete("delete-photo/{photoId}")]
		public async Task<ActionResult> DeletePhoto(int photoId)
		{
			var usernam = User.FindFirstValue(ClaimTypes.Name);
			var appuser = await _UserManager.Users.Include(U => U.Photos).FirstOrDefaultAsync(U => U.UserName == usernam.ToLower());
			if (appuser is null) return NotFound(new ErrorDto(404));
			var photo = appuser.Photos.FirstOrDefault(P => P.Id == photoId);
			if (photo == null) return NotFound(new ErrorDto(404));
			if (photo.IsMain) return BadRequest("You can't delete your main photo");
			appuser.Photos.Remove(photo);
			var res = await _UserManager.UpdateAsync(appuser);
			if (!res.Succeeded) return BadRequest(new ErrorDto(400));
			return NoContent();
		}

	}
}
