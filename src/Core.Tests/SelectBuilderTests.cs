using Dapper.Repository.Core.Attributes;
using Xunit;

namespace Dapper.Repository.Core.Tests
{
	public class SelectBuilderTests
	{
		[Fact]
		public void Empty_Select_Builder_Creates_According_To_Class()
		{
			// Arrage
			const string expectedSql = @"SELECT [Id], [ColumnOne], [ColumnTwo] FROM TestClassOne";
			var selectBuilder = new SelectBuilder<TestClassOne>();

			// Act
			var actualSql = selectBuilder.Build();

			// Assert
			Assert.Equal(expectedSql, actualSql);
		}

		[Fact]
		public void Empty_Select_Builder_Creates_According_To_Class_When_Has_Different_Name()
		{
			// Arrage
			const string expectedSql = @"SELECT [Key] AS [Id], [Column1] AS [ColumnOne], [Column2] AS [ColumnTwo] FROM TestClassTwo";
			var selectBuilder = new SelectBuilder<TestClassTwo>();

			// Act
			var actualSql = selectBuilder.Build();

			// Assert
			Assert.Equal(expectedSql, actualSql);
		}

		[Fact]
		public void Empty_Select_Builder_Creates_According_To_Class_When_Mixed()
		{
			// Arrage
			const string expectedSql = @"SELECT [Id], [Column1] AS [ColumnOne], [ColumnTwo] FROM TestClassThree";
			var selectBuilder = new SelectBuilder<TestClassThree>();

			// Act
			var actualSql = selectBuilder.Build();

			// Assert
			Assert.Equal(expectedSql, actualSql);
		}

		public class TestClassOne
		{
			[Key]
			public int Id { get; set; }

			[Column]
			public string ColumnOne { get; set; }

			[Column]
			public string ColumnTwo { get; set; }
		}

		public class TestClassTwo
		{
			[Key("Key")]
			public int Id { get; set; }

			[Column("Column1")]
			public string ColumnOne { get; set; }

			[Column("Column2")]
			public string ColumnTwo { get; set; }
		}

		public class TestClassThree
		{
			[Key]
			public int Id { get; set; }

			[Column("Column1")]
			public string ColumnOne { get; set; }

			[Column]
			public string ColumnTwo { get; set; }
		}
	}

}
