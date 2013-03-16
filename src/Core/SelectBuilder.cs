using System.Linq;
using System.Text;

namespace Dapper.Repository.Core
{
	// TODO : Change this name....FluentBuilder
	// TODO : Have different builders.....
	public static class SelectBuilder
	{
		// TODO : Flesh this out...
		// TODO : How can this be better managed?
		public static Where<T> Where<T>(this Select<T> selectBuilder) where T : class
		{
			// TODO : Should where be a property on select????
			return new Where<T>();
		}

		// TODO : Do this in a more structured way.....
		// TODO : Need to accept differnet types...
		// TODO : Should this be here or part of the select class? 
		public static string Build<T>(this Select<T> selectBuilder) where T : class
		{
			var builder = new StringBuilder();

			builder.Append("SELECT ");

			// TODO : Better generic way of getting columns...
			if (selectBuilder.Key.Key != null)
			{
				builder.Append(selectBuilder.Key.Key == selectBuilder.Key.Value
					               ? string.Format("[{0}]", selectBuilder.Key.Key)
					               : string.Format("[{0}] AS [{1}]", selectBuilder.Key.Value, selectBuilder.Key.Key));

				if (selectBuilder.Columns.Any())
				{
					builder.Append(", ");
				}
			}

			builder.Append(string.Join(", ", selectBuilder.Columns.Select(
				s => s.Key == s.Value ?
					string.Format("[{0}]", s.Key) :
					string.Format("[{0}] AS [{1}]", s.Value, s.Key)
					)));

			builder.Append(string.Format(" FROM {0}", selectBuilder.Table));

			// TODO : Need where clauses somehow...

			return builder.ToString();
		}
	}
}