using System;

using Xunit;
using FluentAssertions;

namespace SixtenLabs.Simulacrum.Tests
{
	public class AspectTests
	{
		private Aspect NewSubjectUnderTest(int size)
		{
			return new Aspect(size);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public void IsSet_NoBitsSet_ReturnsFalse(int indexToTest)
		{
			var subject = NewSubjectUnderTest(3);

			var actual = subject.HasMask(indexToTest);

			actual.Should().BeFalse();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(5)]
		[InlineData(int.MaxValue)]
		public void IsSet_IndexOutOfRange_ThrowsException(int indexToTest)
		{
			var subject = NewSubjectUnderTest(5);

			Action act = () => subject.HasMask(indexToTest);

			act.ShouldThrow<ArgumentOutOfRangeException>();
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public void Set_PositiveIndex_BitIsSet(int indexToTest)
		{
			var subject = NewSubjectUnderTest(3);

			subject.AddMask(indexToTest);

			subject.HasMask(indexToTest).Should().BeTrue();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		public void Set_NegativeIndex_ThrowsException(int indexToTest)
		{
			var subject = NewSubjectUnderTest(1);

			Action act = () => subject.AddMask(indexToTest);

			act.ShouldThrow<ArgumentOutOfRangeException>();
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public void ClearBit_NoBitsSet_ReturnsFalse(int indexToTest)
		{
			var subject = NewSubjectUnderTest(3);

			subject.RemoveMask(indexToTest);

			subject.HasMask(indexToTest).Should().BeFalse();
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public void ClearBit_BitsSet_ReturnsFalse(int indexToTest)
		{
			var subject = NewSubjectUnderTest(3);

			subject.AddMask(indexToTest);
			subject.HasMask(indexToTest).Should().BeTrue();
			subject.RemoveMask(indexToTest);

			subject.HasMask(indexToTest).Should().BeFalse();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		public void ClearBit_NegativeIndex_ThrowsException(int indexToTest)
		{
			var subject = NewSubjectUnderTest(1);

			Action act = () => subject.RemoveMask(indexToTest);

			act.ShouldThrow<ArgumentOutOfRangeException>();
		}

		[Theory]
		[InlineData(1, 2, 1, 2, 3, true)]
		[InlineData(3, 5, 3, 4, 5, true)]
		[InlineData(1, 2, 3, 4, 5, false)]
		[InlineData(1, 4, 3, 4, 5, false)]
		[InlineData(1, 5, 3, 4, 5, false)]
		public void IsSubSetOf_WhenCalled_ReturnsCorrectResult(int index1, int index2, int index3, int index4, int index5, bool result)
		{
			var subject = NewSubjectUnderTest(6);

			subject.AddMask(index1);
			subject.AddMask(index2);

			var aspect = NewSubjectUnderTest(6);

			aspect.AddMask(index3);
			aspect.AddMask(index4);
			aspect.AddMask(index5);

			var actual = subject.IsSubsetOf(aspect);

			actual.Should().Be(result);
		}

		[Fact]
		public void Constructor_AllBits_False()
		{
			var subject = NewSubjectUnderTest(4);

			subject.HasMask(0).Should().BeFalse();
			subject.HasMask(1).Should().BeFalse();
			subject.HasMask(2).Should().BeFalse();
			subject.HasMask(3).Should().BeFalse();
		}
	}
}
