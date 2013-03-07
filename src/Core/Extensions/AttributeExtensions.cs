using System;
using System.Collections.Generic;
using System.Linq;
using Dapper.Repository.Core.Attributes;

namespace Dapper.Repository.Core.Extensions
{
	public static class AttributeExtensions
	{
		public static string GetTableName(this Type type)
		{
			var attribute = type.GetCustomAttributes(typeof(Table), true).FirstOrDefault() as Table;

			return attribute != null && !String.IsNullOrWhiteSpace(attribute.Name) ? attribute.Name : type.Name;
		}

		public static KeyValuePair<string, string> GetKeyName(this Type type)
		{
			foreach (var property in type.GetProperties())
			{
				var attribute = property.GetCustomAttributes(typeof(Key), true).FirstOrDefault() as Key;

				if (attribute == null) continue;

				return !String.IsNullOrWhiteSpace(attribute.Name) ?
					new KeyValuePair<string, string>( property.Name, attribute.Name) :
					new KeyValuePair<string, string>(property.Name, property.Name);
			}

			throw new MissingFieldException("No key provided");
		}

		public static IEnumerable<KeyValuePair<string, string>> GetColumnNames(this Type type)
		{
			foreach (var property in type.GetProperties())
			{
				var attribute = property.GetCustomAttributes(typeof(Column), true).FirstOrDefault() as Column;

				if (attribute == null) continue;

				if (!String.IsNullOrWhiteSpace(attribute.Name))
				{
					yield return new KeyValuePair<string, string>(property.Name, attribute.Name);
				}
				else
				{
					yield return new KeyValuePair<string, string>(property.Name, property.Name);
				}
			}
		}
	}
}