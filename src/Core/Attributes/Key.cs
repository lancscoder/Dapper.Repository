using System;

namespace Dapper.Repository.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class Key : Attribute
	{
		private readonly string _name;

		public string Name { get { return _name; } }

		public Key()
		{
		}

		public Key(string name)
		{
			_name = name;
		}
	}
}