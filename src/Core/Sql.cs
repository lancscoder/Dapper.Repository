using Dapper.Repository.Core.Attributes;

namespace Dapper.Repository.Core
{
	public static class Sql<T> where T : class 
	{
		public static string Get()
		{
			return "";
		}
	}

	public class DummyClass
	{
		public void Do()
		{
			Sql<TestClass>.Get();
		}
	}

	public class TestClass
	{
		[Key]
		public int Id { get; set; }
	}
}
