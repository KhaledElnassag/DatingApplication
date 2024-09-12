using DatingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Interfaces
{
	public interface ITokenService
	{
		Task<string> GenerateTokenAsync(ApplicationUser user);
	}
}
