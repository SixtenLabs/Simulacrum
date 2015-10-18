using Xunit;
using FluentAssertions;

namespace SixtenLabs.Simulacrum.Tests
{
	public class BagTests
	{
		private Bag<T> NewSubjectUnderTest<T>(int initialSize)
		{
			return new Bag<T>(initialSize);
		}

		[Fact]
		public void Count_NoValues_ReturnsZero()
		{
			var subject = NewSubjectUnderTest<int>(10);

			subject.Count.Should().Be(0);
		}

		[Fact]
		public void IsEmpty_NoValues_ReturnsTrue()
		{
			var subject = NewSubjectUnderTest<int>(10);

			subject.IsEmpty.Should().BeTrue();
		}

		[Fact]
		public void Capacity_Returns_CorrectBagSize()
		{
			var subject = NewSubjectUnderTest<int>(10);

			subject.Capacity.Should().Be(10);
		}

		[Fact]
		public void Add_Returns_CorrectBagSize()
		{
			var subject = NewSubjectUnderTest<int>(10);

			subject.Add(1);

			subject.Capacity.Should().Be(10);
		}
	}
}
