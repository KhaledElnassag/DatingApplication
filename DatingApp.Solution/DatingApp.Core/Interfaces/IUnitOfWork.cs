using DatingApp.Core.Models;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Interfaces
{
	public interface IUnitOfWork:IAsyncDisposable
	{
		IGenericeRepository<T>? GetRepository<T>() where T:BaseEntity;
		Task<int> CompleteAsync();
	}
}
