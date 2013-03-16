using System;

namespace Dapper.Repository.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class Key : GenericTableAttribute
	{
		public Key()
		{
		}

		public Key(string name)
			: base(name)
		{
		}
	}
}