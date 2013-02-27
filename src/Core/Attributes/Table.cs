using System;

namespace Dapper.Repository.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class Table : Attribute
	{
		private readonly string _name;

		public string Name { get { return _name; } }

		public Table()
		{
		}

		public Table(string name)
		{
			_name = name;
		}
	}
}