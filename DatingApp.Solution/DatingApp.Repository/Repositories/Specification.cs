using DatingApp.Core.Interfaces;
using DatingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Repository.Repositories
{
	public class Specification<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>>? FilterCrietria { get; set;}
		public List<Expression<Func<T, object>>>? IncludeCrietria { get; set; }=new List<Expression<Func<T, object>>>(){ };
		public Expression<Func<T, object>>? OrderByCrietria {get; set;}
		public List<Expression<Func<T, object>>>? ThenByCrietria { get; set;}
		public Expression<Func<T, object>>? OrderByDesCrietria {get; set;}
		public List<Expression<Func<T, object>>>? ThenByDesCrietria {get; set;}
	}
}
