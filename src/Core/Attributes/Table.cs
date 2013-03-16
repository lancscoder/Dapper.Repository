using System;

namespace Dapper.Repository.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class Table : GenericTableAttribute
	{
		public Table()
		{
		}

		public Table(string name)
			: base(name)
		{
		}
	}
}