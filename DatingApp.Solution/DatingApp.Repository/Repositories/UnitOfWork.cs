using DatingApp.Core.Interfaces;
using DatingApp.Core.Models;
using DatingApp.Repository.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Repository.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationContext _Context;
        private Dictionary<string,object> Repositories { get; set; }

        public UnitOfWork(ApplicationContext context)
        {
			_Context = context;
			Repositories = new Dictionary<string,object>();
		}
		public IGenericeRepository<T>? GetRepository<T>() where T : BaseEntity
		{
			string ClassName = typeof(T).Name;
			if(!Repositories.ContainsKey(ClassName))
				Repositories.Add(ClassName, new GenericRepository<T>(_Context));
			return Repositories[ClassName] as IGenericeRepository<T>;
		}
		public async Task<int> CompleteAsync()
		{
			return await _Context.SaveChangesAsync();
		}

		public async ValueTask DisposeAsync()
		{
			 await _Context.DisposeAsync();
		}

		
	}
}
