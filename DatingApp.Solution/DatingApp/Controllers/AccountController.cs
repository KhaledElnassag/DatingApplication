using AutoMapper;
using DatingApp.Core.Dtos;
using DatingApp.Core.Interfaces;
using DatingApp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{

	public class AccountController : BaseApiController
	{
		private readonly ITokenService _TokenService;
		private readonly SignInManager<ApplicationUser> _SignInManager;
		private readonly UserManager<ApplicationUser> _UserManager;
		private readonly IMapper _Mapper;

		public AccountController(ITokenService tokenService,SignInManager<ApplicationUser> signInManager
			, UserManager<ApplicationUser> userManager,IMapper mapper)
		{
			_TokenService = tokenService;
			_SignInManager = signInManager;
			_UserManager = userManager;
			_Mapper = mapper;
		}
		[HttpPost("register")]
		public async Task<ActionResult> register(RegisterDto dto)
		{
			var IsExist =  _UserManager.Users.Any(U=>U.UserName==dto.UserName.ToLower());
			if (!IsExist)
			{
				var User = _Mapper.Map<ApplicationUser>(dto);
				var IsSuccess = await _UserManager.CreateAsync(User, dto.Password);
				if (IsSuccess.Succeeded)
				{
					return Ok(new UserDto
					{
						UserName = User.UserName,
						Token = await _TokenService.GenerateTokenAsync(User),
						PhotoUrl=User.Photos.FirstOrDefault
						(P=>P.IsMain)?.Url
					});
				}
				return BadRequest(new ErrorDto(400,"User Name Or Password InValid!"));
			}
			return BadRequest(new ErrorDto(400,"User Already Exist!"));
		}
		[HttpPost("login")]
		public async Task<ActionResult> Login(LoginDto dto)
		{
			var User = await _UserManager.Users.Include(U=>U.Photos).FirstOrDefaultAsync(U=>U.UserName.ToLower()== dto.UserName.ToLower());
			if (User is not null)
			{
				var IsValid=await _SignInManager.CheckPasswordSignInAsync(User, dto.Password,false);
				if (IsValid.Succeeded)
				{
					var UserToken= await _TokenService.GenerateTokenAsync(User);
					if (UserToken is not null) {
						var res = new UserDto
						{
							UserName = User.UserName,
							Token = UserToken,
							PhotoUrl = User.Photos.FirstOrDefault(P => P.IsMain)?.Url
						};
						return Ok(res); 
					}
				}
			}
			return Unauthorized(new ErrorDto(401));
		}
	}
}
