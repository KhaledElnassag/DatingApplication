using DatingApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DatingApp.SignalR
{
	[Authorize]
	public class PresenceHub:Hub
	{
		private readonly IPresenceService _PresenceService;

		public PresenceHub(IPresenceService presenceService)
        {
			_PresenceService = presenceService;
		}
        public override async Task OnConnectedAsync()
		{
			var userName = Context.User.FindFirstValue(ClaimTypes.Name);
			await _PresenceService.ConnectUser(userName, Context.ConnectionId);
			 await Clients.Others.SendAsync("UserOnlion",userName);
			var activeUsers = await _PresenceService.GetOnlineUser();
			 await Clients.Others.SendAsync("GetOnlionUsers",activeUsers);
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			var userName = Context.User.FindFirstValue(ClaimTypes.Name);
			await _PresenceService.DisConnectUser(userName, Context.ConnectionId);
			await Clients.Others.SendAsync("UserOfflion", userName);
			var activeUsers = await _PresenceService.GetOnlineUser();
			await Clients.Others.SendAsync("GetOnlionUsers", activeUsers);
			await base.OnDisconnectedAsync(exception);
		}
	}
}
