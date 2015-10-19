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
	}
}
