using Dapper.Repository.Core.Attributes;

namespace Dapper.Repository.Core
{
	public static class Sql<T> where T : class 
	{
		public static SelectBuilder<T> Get()
		{
			return new SelectBuilder<T>();
		}
	}
}
