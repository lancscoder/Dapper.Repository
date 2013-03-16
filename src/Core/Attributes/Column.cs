using System;

namespace Dapper.Repository.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class Column : GenericTableAttribute
	{
		public Column()
		{
		}

		public Column(string name)
			: base(name)
		{
		}
	}
}