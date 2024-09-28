using DatingApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Service
{
	public class PresenceService: IPresenceService
	{
		private Dictionary<string, List<string>> presence=new Dictionary<string, List<string>>();
		public Task ConnectUser(string userName,string connectioId)
		{
			lock (presence) { 
			if(presence.ContainsKey(userName))
					presence[userName].Add(connectioId);
			else
					presence.Add(userName, new List<string> { connectioId});
			}
		return Task.CompletedTask; 
		}
		public Task DisConnectUser(string userName, string connectioId)
		{
			lock (presence)
			{
				if (presence.ContainsKey(userName))
				{
					presence[userName].Remove(connectioId);
					if (presence[userName].Count == 0)
						presence.Remove(userName);
				}
			}
			return Task.CompletedTask;
		}
		public  Task<List<string>> GetOnlineUser()
		{
			var onlineUsers= new List<string>();
			lock (presence)
			{
				onlineUsers=presence.Select(p=>p.Key).ToList();
			}
			return Task.FromResult( onlineUsers);
		}
	}
}
