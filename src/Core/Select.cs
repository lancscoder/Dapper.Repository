using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Dapper.Repository.Core.Attributes;
using Dapper.Repository.Core.Extensions;

namespace Dapper.Repository.Core
{
	// Question: Show the class specify the columns or ignores?

	public class Select<T> where T : class
	{
		public string Table { get; private set; }
		public KeyValuePair<string, string> Key { get; private set; }
		public IEnumerable<KeyValuePair<string, string>> Columns { get; private set; }

		// TODO : expand the columns to accept other select builders....
		public Select(params Expression<Func<T, object>>[] columns)
		{
			if (columns.Any())
			{
				// Note : Keys arent used here......
				// Are the keys needed? Or is that just in update/insert? Maybe the where as well...
				Key = ProcessUserDefinedColumnsForKey(columns);
				Columns = ProcessUserDefinedColumns(columns);
			}
			else
			{
				Key = typeof(T).GetKeyName();
				Columns = typeof(T).GetColumnNames();
			}

			Table = typeof(T).GetTableName();
		}

		// TODO : Rewrite all this and make more generic...

		private KeyValuePair<string, string> ProcessMember(MemberInfo member, Type requiredType)
		{
			foreach (var attribute in member.GetCustomAttributes(true))
			{
				var type = attribute.GetType();

				if (type == requiredType)
				{
					var columnAttribute = attribute as GenericTableAttribute;

					if (columnAttribute == null) continue;

					return columnAttribute.GetKeyValueDetails(member.Name);
				}
			}

			return new KeyValuePair<string, string>(null, null);
		}

		private KeyValuePair<string, string> ProcessUserDefinedColumnsForKey(IEnumerable<Expression<Func<T, object>>> columns)
		{
			foreach (var column in columns)
			{
				MemberExpression memberExpression = null;
				if (column.Body.NodeType == ExpressionType.Convert)
				{
					var body = (UnaryExpression)column.Body;
					memberExpression = body.Operand as MemberExpression;
				}
				else if (column.Body.NodeType == ExpressionType.MemberAccess)
				{
					memberExpression = column.Body as MemberExpression;
				}

				if (memberExpression == null) continue;

				var member = memberExpression.Member;

				var columnKey = ProcessMember(member, typeof(Key));

				// If null might be next column...
				if (columnKey.Key != null)
				{
					return columnKey;
				}
			}

			return new KeyValuePair<string, string>(null, null); ;
		}

		// TODO : Tidy up....
		// TODO : This includes primary keys... should it?
		private IEnumerable<KeyValuePair<string, string>> ProcessUserDefinedColumns(IEnumerable<Expression<Func<T, object>>> columns)
		{
			foreach (var column in columns)
			{
				MemberExpression memberExpression = null;
				if (column.Body.NodeType == ExpressionType.Convert)
				{
					var body = (UnaryExpression)column.Body;
					memberExpression = body.Operand as MemberExpression;
				}
				else if (column.Body.NodeType == ExpressionType.MemberAccess)
				{
					memberExpression = column.Body as MemberExpression;
				}

				if (memberExpression == null) continue;

				var member = memberExpression.Member;

				var columnDetails = ProcessMember(member, typeof(Column));

				if (columnDetails.Key != null)
				{
					yield return columnDetails;
				}
			}
		}





		// TODO : 1. Need to specify columns
		// TODO : 2. Need to specify count
		// TODO : 3. Need to specify top
		// TODO : 3.1. Need to get first
		// TODO : 4. Need to specify skip
		// TODO : 5. Need to specify sub queries
	}


	//public static class PropertyHelper<T>
	//{
	//	public static PropertyInfo GetProperty<TValue>(
	//		Expression<Func<T, TValue>> selector)
	//	{
	//		Expression body = selector;
	//		if (body is LambdaExpression)
	//		{
	//			body = ((LambdaExpression)body).Body;
	//		}
	//		switch (body.NodeType)
	//		{
	//			case ExpressionType.MemberAccess:
	//				return (PropertyInfo)((MemberExpression)body).Member;
	//			default:
	//				throw new InvalidOperationException();
	//		}
	//	}
	//}
	//
	//public class T
	//{
	//	public int I { get; set; }
	//}
	//
	//public class Z
	//{
	//	public Z()
	//	{
	//		new Select<T>(arg => arg.I);
	//	}
	//}

}