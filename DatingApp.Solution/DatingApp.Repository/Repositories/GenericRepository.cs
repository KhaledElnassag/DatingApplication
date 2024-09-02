using DatingApp.Core.Interfaces;
using DatingApp.Core.Models;
using DatingApp.Repository.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Repository.Repositories
{
	public class GenericRepository<T> : IGenericeRepository<T> where T : BaseEntity
	{
		private readonly ApplicationContext _Context;

		public GenericRepository(ApplicationContext context)
        {
			_Context = context;
		}
        public async Task<T?> GetByIdAsync(int id)
		{
			return await _Context.Set<T>().FindAsync(id);
		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _Context.Set<T>().ToListAsync();
		}
		public async Task AddAsync(T entity)
		{
			 await _Context.Set<T>().AddAsync(entity);
		}
		public void Update(T entity)
		{
			 _Context.Set<T>().Update(entity);
		}
		public void Delete(T entity)
		{
			 _Context.Set<T>().Remove(entity);
		}

		#region Strategy
		public async Task<TOut?> GetWithSpecAsync<TOut>(Func<IQueryable<T>, IQueryable<TOut>> buildQuery) where TOut : class
		{
			return await buildQuery(_Context.Set<T>()).FirstOrDefaultAsync();
		}

		public async Task<int> CountWithSpecAsync<TOut>(Func<IQueryable<T>, IQueryable<TOut>> buildQuery) where TOut : class
		{
			return await buildQuery(_Context.Set<T>()).CountAsync();
		}

		public async Task<bool> IsExistWithSpecAsync<TOut>(Func<IQueryable<T>, IQueryable<TOut>> buildQuery) where TOut : class
		{
			return await buildQuery(_Context.Set<T>()).AnyAsync();
		}

		public async Task<IEnumerable<TOut>> GetAllWithSpecAsync<TOut>(Func<IQueryable<T>, IQueryable<TOut>> buildQuery) where TOut : class
		{
			return await buildQuery(_Context.Set<T>()).ToListAsync();
		}

		#endregion
		#region Specification
		public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpecification.Apply(_Context.Set<T>(), spec).FirstOrDefaultAsync();
		}

		public async Task<int> CountWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpecification.Apply(_Context.Set<T>(), spec).CountAsync();
		}

		public async Task<bool> IsExistWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpecification.Apply(_Context.Set<T>(), spec).AnyAsync();
		}

		public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpecification.Apply(_Context.Set<T>(), spec).ToListAsync();
		}

		#endregion
		#region Specification With Strategy
		public async Task<TOut?> GetWithSpecAsync<TOut>(ISpecification<T> Specs, Func<IQueryable<T>, IQueryable<TOut>> BuildQuery) where TOut : class
		{
			var SpecQuery = ApplySpecification.Apply(_Context.Set<T>(), Specs);

			return await BuildQuery(SpecQuery).FirstOrDefaultAsync();

		}
		public async Task<IEnumerable<TOut?>> GetAllWithSpecAsync<TOut>(ISpecification<T> Specs, Func<IQueryable<T>, IQueryable<TOut>> BuildQuery) where TOut : class
		{
			var SpecQuery = ApplySpecification.Apply(_Context.Set<T>(), Specs);

			return await BuildQuery(SpecQuery).ToListAsync();

		}
		public async Task<int> CountWithSpecAsync<TOut>(ISpecification<T> Specs, Func<IQueryable<T>, IQueryable<TOut>> BuildQuery) where TOut : class
		{
			var SpecQuery = ApplySpecification.Apply(_Context.Set<T>(), Specs);

			return await BuildQuery(SpecQuery).CountAsync();
		}
		public async Task<bool> IsExistWithSpecAsync<TOut>(ISpecification<T> Specs, Func<IQueryable<T>, IQueryable<TOut>> BuildQuery) where TOut : class
		{
			var SpecQuery = ApplySpecification.Apply(_Context.Set<T>(), Specs);

			return await BuildQuery(SpecQuery).AnyAsync();
		}
		#endregion




	}
}
