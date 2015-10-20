using System;

using Xunit;
using FluentAssertions;

namespace SixtenLabs.Simulacrum.Tests
{
	public class EntityHandleTests
	{
		private EntityHandle NewSubjectUnderTest(Guid entity, int nextIndex, int aspectSize)
		{
			return new EntityHandle(entity, nextIndex, 2);
		}

		[Fact]
		public void Constructor_Entity_IsCorrect()
		{
			var entity = Guid.NewGuid();
			var subject = NewSubjectUnderTest(entity, 1, 2);

			subject.Entity.Should().Be(entity);
		}

		[Fact]
		public void Constructor_Index_IsCorrect()
		{
			var entity = Guid.NewGuid();
			var subject = NewSubjectUnderTest(entity, 1, 2);

			subject.Index.Should().Be(1);
		}

		[Fact]
		public void Aspect_AddMask_AspectHasMask()
		{
			var entity = Guid.NewGuid();
			var subject = NewSubjectUnderTest(entity, 1, 2);

			subject.Aspect.AddMask(0);

			subject.Aspect.HasMask(0).Should().BeTrue();
		}

		[Fact]
		public void Performance_SpinUp10000_LessThan200ms()
		{
			Action someAction = () => SpinUpABunchOfEntityHandles(100000);
      someAction.ExecutionTime().ShouldNotExceed(200.Milliseconds()); 
		}


		private void SpinUpABunchOfEntityHandles(int count)
		{
			for(int i = 0; i < count; i++)
			{
				NewSubjectUnderTest(Guid.NewGuid(), 1, 2);
			}
		}

	}
}
