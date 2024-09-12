using DatingApp.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
	[ApiExplorerSettings(IgnoreApi = true)]
	[ApiController]
	[Route("Error/{code}")]
	public class ErrorController : ControllerBase
	{
		public ActionResult<ErrorDto> Error(int code)
		{
			return BadRequest(new ErrorDto(code));
		}
	}
}
