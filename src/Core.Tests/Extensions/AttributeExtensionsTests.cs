using System;
using System.Linq;
using Dapper.Repository.Core.Attributes;
using Dapper.Repository.Core.Extensions;
using Xunit;

namespace Dapper.Repository.Core.Tests.Extensions
{
	public class AttributeExtensionsTests
	{
		[Fact]
		public void GetTableName_Return_Table_Name_When_Set()
		{
			// Arrange
			var c = typeof (ClassOne);
			var expectedName = "TableOne";

			// Act
			var actualName = c.GetTableName();

			// Assert
			Assert.Equal(expectedName, actualName);
		}

		[Fact]
		public void GetTableName_Return_Class_Name_When_No_Name_Set()
		{
			// Arrange
			var c = typeof(ClassTwo);
			var expectedName = "ClassTwo";

			// Act
			var actualName = c.GetTableName();

			// Assert
			Assert.Equal(expectedName, actualName);
		}

		[Fact]
		public void GetTableName_Return_Class_Name_When_No_Attribute()
		{
			// Arrange
			var c = typeof(ClassThree);
			var expectedName = "ClassThree";

			// Act
			var actualName = c.GetTableName();

			// Assert
			Assert.Equal(expectedName, actualName);
		}

		[Fact]
		public void GetKeyName_Return_Key_Name_When_Set()
		{
			// Arrange
			var c = typeof(ClassOne);
			var expectedKey = "Id";
			var expectedValue = "Key";

			// Act
			var actual = c.GetKeyName();

			// Assert
			Assert.Equal(expectedKey, actual.Key);
			Assert.Equal(expectedValue, actual.Value);
		}

		[Fact]
		public void GetKeyName_Return_Property_Name_When_No_Name_Set()
		{
			// Arrange
			var c = typeof(ClassTwo);
			var expectedKey = "Id";
			var expectedValue = "Id";

			// Act
			var actual = c.GetKeyName();

			// Assert
			Assert.Equal(expectedKey, actual.Key);
			Assert.Equal(expectedValue, actual.Value);
		}

		[Fact]
		public void GetKeyName_Throws_Exception_When_Not_Set()
		{
			// Arrange
			var c = typeof(ClassThree);

			// Act

			// Assert
			Assert.Throws<MissingFieldException>(() =>
				{
					c.GetKeyName();
				});
		}

		[Fact]
		public void GetColumnNames_Return_Column_Names_When_Set()
		{
			// Arrange
			var c = typeof(ClassOne);
			var expectedCount = 2;

			// Act
			var names = c.GetColumnNames().ToArray();

			// Assert
			Assert.Equal(expectedCount, names.Count());
			Assert.Equal("ColumnOne", names[0].Key);
			Assert.Equal("Column1", names[0].Value);
			Assert.Equal("ColumnTwo", names[1].Key);
			Assert.Equal("Column2", names[1].Value);
		}

		[Fact]
		public void GetColumnNames_Return_Column_Names_When_No_Names_Set()
		{
			// Arrange
			var c = typeof(ClassTwo);
			var expectedCount = 2;

			// Act
			var names = c.GetColumnNames().ToArray();

			// Assert
			Assert.Equal(expectedCount, names.Count());
			Assert.Equal("ColumnOne", names[0].Key);
			Assert.Equal("ColumnOne", names[0].Value);
			Assert.Equal("ColumnTwo", names[1].Key);
			Assert.Equal("ColumnTwo", names[1].Value);
		}

		[Fact]
		public void GetColumnNames_Return_Columns_Name_When_Mixed()
		{
			// Arrange
			var c = typeof(ClassThree);
			var expectedCount = 2;

			// Act
			var names = c.GetColumnNames().ToArray();

			// Assert
			Assert.Equal(expectedCount, names.Count());
			Assert.Equal("ColumnOne", names[0].Key);
			Assert.Equal("Column1", names[0].Value);
			Assert.Equal("ColumnTwo", names[1].Key);
			Assert.Equal("ColumnTwo", names[1].Value);
		}

		[Table("TableOne")]
		public class ClassOne
		{
			[Key("Key")]
			public int Id { get; set; }

			[Column("Column1")]
			public string ColumnOne { get; set; }

			[Column("Column2")]
			public string ColumnTwo { get; set; }
		}

		[Table]
		public class ClassTwo
		{
			[Key]
			public int Id { get; set; }

			[Column]
			public string ColumnOne { get; set; }

			[Column]
			public string ColumnTwo { get; set; }
		}

		public class ClassThree
		{
			[Column("Column1")]
			public string ColumnOne { get; set; }

			[Column]
			public string ColumnTwo { get; set; }
		}
	}
}