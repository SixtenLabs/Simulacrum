using System;

using Xunit;
using FluentAssertions;

namespace SixtenLabs.Simulacrum.Tests
{
	public class AspectTests
	{
		private Aspect NewSubjectUnderTest()
		{
			return new Aspect();
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(int.MaxValue)]
		public void IsSet_NoBitsSet_ReturnsFalse(int indexToTest)
		{
			var subject = NewSubjectUnderTest();

			var actual = subject.IsSet(indexToTest);

			actual.Should().BeFalse();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		public void IsSet_NegativeIndex_ThrowsException(int indexToTest)
		{
			var subject = NewSubjectUnderTest();

			Action act = () => subject.IsSet(indexToTest);

			act.ShouldThrow<ArgumentOutOfRangeException>();
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public void SetBit_PositiveIndex_BitIsSet(int indexToTest)
		{
			var subject = NewSubjectUnderTest();

			subject.SetBit(indexToTest);

			subject.IsSet(indexToTest).Should().BeTrue();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		public void SetBit_NegativeIndex_ThrowsException(int indexToTest)
		{
			var subject = NewSubjectUnderTest();

			Action act = () => subject.SetBit(indexToTest);

			act.ShouldThrow<ArgumentOutOfRangeException>();
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public void ClearBit_NoBitsSet_ReturnsFalse(int indexToTest)
		{
			var subject = NewSubjectUnderTest();

			subject.SetBit(indexToTest, false);

			subject.IsSet(indexToTest).Should().BeFalse();
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public void ClearBit_BitsSet_ReturnsFalse(int indexToTest)
		{
			var subject = NewSubjectUnderTest();

			subject.SetBit(indexToTest);
			subject.IsSet(indexToTest).Should().BeTrue();
			subject.SetBit(indexToTest, false);

			subject.IsSet(indexToTest).Should().BeFalse();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		public void ClearBit_NegativeIndex_ThrowsException(int indexToTest)
		{
			var subject = NewSubjectUnderTest();

			Action act = () => subject.SetBit(indexToTest, false);

			act.ShouldThrow<ArgumentOutOfRangeException>();
		}

		[Fact]
		public void ClearAll_BitsSet_AllBitsEmpty()
		{
			var subject = NewSubjectUnderTest();

			subject.SetBit(0);
			subject.SetBit(1);
			subject.SetBit(2);
			subject.SetBit(3);
			//subject.SetBit(int.MaxValue);

			subject.ClearAll();

			subject.IsSet(0).Should().BeFalse();
			subject.IsSet(1).Should().BeFalse();
			subject.IsSet(2).Should().BeFalse();
			subject.IsSet(3).Should().BeFalse();
			subject.IsSet(int.MaxValue).Should().BeFalse();
		}

		[Fact]
		public void SetAll_BitsSet_AllBitsSet()
		{
			var subject = NewSubjectUnderTest();

			subject.SetBit(0);
			subject.SetBit(1);
			subject.SetBit(2);
			subject.SetBit(3);

			subject.ClearAll();

			subject.IsSet(0).Should().BeFalse();
			subject.IsSet(1).Should().BeFalse();
			subject.IsSet(2).Should().BeFalse();
			subject.IsSet(3).Should().BeFalse();
			subject.IsSet(int.MaxValue).Should().BeFalse();

			subject.SetAll();

			subject.IsSet(0).Should().BeTrue();
			subject.IsSet(1).Should().BeTrue();
			subject.IsSet(2).Should().BeTrue();
			subject.IsSet(3).Should().BeTrue();
		}

		[Theory]
		[InlineData(1, 2, 1, 2, 3, true)]
		[InlineData(3, 5, 3, 4, 5, true)]
		[InlineData(1, 2, 3, 4, 5, false)]
		[InlineData(1, 4, 3, 4, 5, false)]
		[InlineData(1, 5, 3, 4, 5, false)]
		public void IsSubSetOf_WhenCalled_ReturnsCorrectResult(int index1, int index2, int index3, int index4, int index5, bool result)
		{
			var subject = NewSubjectUnderTest();

			subject.SetBit(index1);
			subject.SetBit(index2);

			var aspect = NewSubjectUnderTest();

			aspect.SetBit(index3);
			aspect.SetBit(index4);
			aspect.SetBit(index5);

			var actual = subject.IsSubsetOf(aspect);

			actual.Should().Be(result);
		}
	}
}
