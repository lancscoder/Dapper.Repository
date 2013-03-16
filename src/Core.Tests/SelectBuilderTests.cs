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
			var sql = Sql<TestClassOne>.Get();

			// Act
			var actualSql = sql.Build();

			// Assert
			Assert.Equal(expectedSql, actualSql);
		}

		[Fact]
		public void Empty_Select_Builder_Creates_According_To_Class_When_Has_Different_Name()
		{
			// Arrage
			const string expectedSql = @"SELECT [Key] AS [Id], [Column1] AS [ColumnOne], [Column2] AS [ColumnTwo] FROM TestClass2";
			var sql = Sql<TestClassTwo>.Get();

			// Act
			var actualSql = sql.Build();

			// Assert
			Assert.Equal(expectedSql, actualSql);
		}

		[Fact]
		public void Empty_Select_Builder_Creates_According_To_Class_When_Mixed()
		{
			// Arrage
			const string expectedSql = @"SELECT [Id], [Column1] AS [ColumnOne], [ColumnTwo] FROM TestClassThree";
			var sql = Sql<TestClassThree>.Get();

			// Act
			var actualSql = sql.Build();

			// Assert
			Assert.Equal(expectedSql, actualSql);
		}

		[Fact]
		public void Select_Builder_Creates_According_To_Single_Column_Passed()
		{
			// Arrage
			const string expectedSql = @"SELECT [ColumnOne] FROM TestClassOne";
			var sql = Sql<TestClassOne>.Get(c => c.ColumnOne);

			// Act
			var actualSql = sql.Build();

			// Assert
			Assert.Equal(expectedSql, actualSql);
		}

		[Fact]
		public void Select_Builder_Creates_According_To_Key_Passed()
		{
			// Arrage
			const string expectedSql = @"SELECT [Id] FROM TestClassOne";
			var sql = Sql<TestClassOne>.Get(c => c.Id);

			// Act
			var actualSql = sql.Build();

			// Assert
			Assert.Equal(expectedSql, actualSql);
		}

		[Fact]
		public void Select_Builder_Creates_According_To_Columns_Passed()
		{
			// Arrage
			const string expectedSql = @"SELECT [Id], [ColumnOne] FROM TestClassOne";
			var sql = Sql<TestClassOne>.Get(c => c.Id, c => c.ColumnOne);

			// Act
			var actualSql = sql.Build();

			// Assert
			Assert.Equal(expectedSql, actualSql);
		}

		[Fact]
		public void Select_Builder_Creates_According_To_Columns_Passed_With_Alternative_Names()
		{
			// Arrage
			const string expectedSql = @"SELECT [Key] AS [Id], [Column1] AS [ColumnOne] FROM TestClass2";
			var sql = Sql<TestClassTwo>.Get(c => c.Id, c => c.ColumnOne);

			// Act
			var actualSql = sql.Build();

			// Assert
			Assert.Equal(expectedSql, actualSql);
		}

		// Dummy classes for testing

		public class TestClassOne
		{
			[Key]
			public int Id { get; set; }

			[Column]
			public string ColumnOne { get; set; }

			[Column]
			public string ColumnTwo { get; set; }
		}

		[Table("TestClass2")]
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
