using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Dapper.Repository.Core.Extensions;

namespace Dapper.Repository.Core
{
	public class SelectBuilder<T> where T : class
	{
		public string Table { get; private set; }
		public KeyValuePair<string, string> Key { get; private set; }
		public IEnumerable<KeyValuePair<string, string>> Columns { get; private set; }

		public SelectBuilder(params Expression<Func<T, TValue>>[] columns) 
		{
			Table = typeof(T).GetTableName();
			Key = typeof(T).GetKeyName();
			Columns = typeof(T).GetColumnNames();
		}

		// TODO : 1. Need to specify columns
		// TODO : 2. Need to specify count
		// TODO : 3. Need to specify top
		// TODO : 3.1. Need to get first
		// TODO : 4. Need to specify skip
		// TODO : 5. Need to specify sub queries
	}


	public static class PropertyHelper<T>
	{
		public static PropertyInfo GetProperty<TValue>(
			Expression<Func<T, TValue>> selector)
		{
			Expression body = selector;
			if (body is LambdaExpression)
			{
				body = ((LambdaExpression)body).Body;
			}
			switch (body.NodeType)
			{
				case ExpressionType.MemberAccess:
					return (PropertyInfo)((MemberExpression)body).Member;
				default:
					throw new InvalidOperationException();
			}
		}
	}

	public class T
	{
		public int I { get; set; }
	}

	public class Z
	{
		public Z()
		{
			new SelectBuilder<T>(arg => arg.I);
		}
	}

}