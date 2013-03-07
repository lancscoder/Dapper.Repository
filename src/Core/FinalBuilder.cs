using System.Linq;
using System.Text;

namespace Dapper.Repository.Core
{
	public static class FinalBuilder
	{
		public static string Build<T>(this SelectBuilder<T> selectBuilder) where T : class
		{
			var builder = new StringBuilder();

			builder.Append("SELECT ");

			if (selectBuilder.Key.Key == selectBuilder.Key.Value)
			{
				builder.Append(string.Format("[{0}], ", selectBuilder.Key.Key));
			}
			else
			{
				builder.Append(string.Format("[{0}] AS [{1}], ", selectBuilder.Key.Value, selectBuilder.Key.Key));
			}

			builder.Append(string.Join(", ", selectBuilder.Columns.Select(
				s => s.Key == s.Value ?
					string.Format("[{0}]", s.Key) :
					string.Format("[{0}] AS [{1}]", s.Value, s.Key)
					)));

			builder.Append(string.Format(" FROM {0}", selectBuilder.Table));

			return builder.ToString();
		}
	}
}