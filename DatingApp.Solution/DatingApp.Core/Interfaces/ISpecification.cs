using DatingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Interfaces
{
	public interface ISpecification<T> where T : BaseEntity
	{
         Expression<Func<T,bool>>? FilterCrietria { get; set; }
		List<Expression<Func<T,object>>>? IncludeCrietria { get; set; }
         Expression<Func<T,object>>? OrderByCrietria { get; set; }
		List<Expression<Func<T, object>>>? ThenByCrietria { get; set; }
         Expression<Func<T,object>>? OrderByDesCrietria { get; set; }
		List<Expression<Func<T, object>>>? ThenByDesCrietria { get; set; }
    }
}
