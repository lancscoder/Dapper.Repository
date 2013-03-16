using System;

namespace Dapper.Repository.Core.Attributes
{
	public abstract class GenericTableAttribute : Attribute
	{
		private readonly string _name;

		public string Name { get { return _name; } }

		protected GenericTableAttribute()
		{
		}

		protected GenericTableAttribute(string name)
		{
			_name = name;
		}
	}
}