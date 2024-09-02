using DatingApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Interfaces
{
	public interface IGenericeRepository<T> where T : BaseEntity
	{
		Task<T?> GetByIdAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();
		Task AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
		#region Strategy
		Task<TOut?> GetWithSpecAsync<TOut>(Func<IQueryable<T>, IQueryable<TOut>> buildQuery) where TOut : class;
		Task<int> CountWithSpecAsync<TOut>(Func<IQueryable<T>, IQueryable<TOut>> buildQuery) where TOut : class;
		Task<bool> IsExistWithSpecAsync<TOut>(Func<IQueryable<T>, IQueryable<TOut>> buildQuery) where TOut : class;
		Task<IEnumerable<TOut>> GetAllWithSpecAsync<TOut>(Func<IQueryable<T>, IQueryable<TOut>> buildQuery) where TOut : class;
		#endregion
		#region Specification
		public Task<T?> GetWithSpecAsync(ISpecification<T> spec);

		public Task<int> CountWithSpecAsync(ISpecification<T> spec);

		public Task<bool> IsExistWithSpecAsync(ISpecification<T> spec);

		public Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec); 
		#endregion
		#region Strategy & Specification

		public Task<TOut?> GetWithSpecAsync<TOut>(ISpecification<T> Specs, Func<IQueryable<T>, IQueryable<TOut>> BuildQuery) where TOut : class;
		public Task<IEnumerable<TOut?>> GetAllWithSpecAsync<TOut>(ISpecification<T> Specs, Func<IQueryable<T>, IQueryable<TOut>> BuildQuery) where TOut : class;
		public  Task<int> CountWithSpecAsync<TOut>(ISpecification<T> Specs, Func<IQueryable<T>, IQueryable<TOut>> BuildQuery) where TOut : class;
		public  Task<bool> IsExistWithSpecAsync<TOut>(ISpecification<T> Specs, Func<IQueryable<T>, IQueryable<TOut>> BuildQuery) where TOut : class;
		#endregion



	}
}
