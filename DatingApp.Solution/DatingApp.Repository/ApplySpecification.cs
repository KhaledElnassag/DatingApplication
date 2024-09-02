using DatingApp.Core.Interfaces;
using DatingApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Repository
{
	public static class ApplySpecification
	{
		public static IQueryable<T> Apply<T>(IQueryable<T> query, ISpecification<T> spec)where T :BaseEntity
		{
			if (spec.IncludeCrietria is not null)
			{
				query=spec.IncludeCrietria.Aggregate(query,(q,critria)=>q.Include(critria));
			}
			if (spec.FilterCrietria is not null) {
				query = query.Where(spec.FilterCrietria);
			}
			if (spec.OrderByCrietria is not null)
			{
				query = query.OrderBy(spec.OrderByCrietria);
			}
			if (spec.ThenByCrietria is not null)
			{
				query=spec.ThenByCrietria.Aggregate((IOrderedQueryable<T>)query, (q, critria) => q.ThenBy(critria));
			}
			if (spec.OrderByDesCrietria is not null)
			{
				query = query.OrderByDescending(spec.OrderByDesCrietria);
			}
			if (spec.ThenByDesCrietria is not null)
			{
				query = spec.ThenByDesCrietria.Aggregate((IOrderedQueryable<T>)query, (q, critria) => q.ThenByDescending(critria));
			}
			return query;
		}
	}
}
