using System;

using Xunit;
using FluentAssertions;

namespace SixtenLabs.Simulacrum.Tests
{
	public class BagTests
	{
		private Bag<T> NewSubjectUnderTest<T>()
		{
			return new Bag<T>();
		}

		[Fact]
		public void Constructor_Count_ShouldBeZero()
		{
			var subject = NewSubjectUnderTest<int>();

			subject.Count.Should().Be(0);
		}

		[Fact]
		public void Constructor_Capacity_ShouldBeSixteen()
		{
			var subject = NewSubjectUnderTest<int>();

			subject.Capacity.Should().Be(16);
		}

		[Fact]
		public void Indexer_Set_SetsCorrectValue()
		{
			var subject = NewSubjectUnderTest<int>();

			subject[0] = 1;

			subject[0].Should().Be(1);
		}

		[Fact]
		public void Indexer_SetOutsideRange_CollectionGrows()
		{
			var subject = NewSubjectUnderTest<int>();

			subject[100] = 1;

			subject.Capacity.Should().Be(200);
		}

		[Fact]
		public void Delete_Value_IsDefault()
		{
			var subject = NewSubjectUnderTest<int>();

			subject[1] = 1;

			subject.Delete(1);

			subject[1].Should().Be(default(int));
		}
	}
}
