using System;
using System.Linq.Expressions;

namespace Dapper.Repository.Core
{
	public static class Sql<T> where T : class 
	{
		public static Select<T> Get(params Expression<Func<T, object>>[] columns)
		{
			return new Select<T>(columns);
		}

		// TODO : Count...should this be another property type?
	}
}
