using DatingApp.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
	[ApiExplorerSettings(IgnoreApi = true)]
	[ApiController]
	[Route("Error/{code}")]
	public class ErrorController : Controller
	{
		public ActionResult<ErrorDto> Error(int code)
		{
			return Ok(new ErrorDto(code));
		}
	}
}
