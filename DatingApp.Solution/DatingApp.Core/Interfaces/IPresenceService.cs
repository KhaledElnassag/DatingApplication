using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Interfaces
{
	public interface IPresenceService
	{
		public Task ConnectUser(string userName, string connectioId);
		public Task DisConnectUser(string userName, string connectioId);
		public Task<List<string>> GetOnlineUser();
	}
}
