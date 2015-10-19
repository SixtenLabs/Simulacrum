using Xunit;
using FluentAssertions;

using SixtenLabs.Simulacrum.SampleImplementation;

namespace SixtenLabs.Simulacrum.Tests
{
	public class ComponentManagerTests
	{
		private ComponentManager NewSubjectUnderTest(int size)
		{
			return new ComponentManager(new IComponent[] { new TransformComponent(), new VelocityComponent() });
		}

		[Fact]
		public void Count_Returns_CorrectNumberOfComponents()
		{
			var subject = NewSubjectUnderTest(4);

			subject.Count.Should().Be(2);
		}

		[Fact]
		public void AspectMask_Returns_CorrectMask()
		{
			var subject = NewSubjectUnderTest(4);

			subject.AspectMask(typeof(TransformComponent)).Should().Be(0);
			subject.AspectMask(typeof(VelocityComponent)).Should().Be(1);
		}

		[Fact]
		public void GetComponent_Returns_CorrectNumberOfComponents()
		{
			var subject = NewSubjectUnderTest(4);

			var actual = subject.GetComponent<TransformComponent>();

			actual.Should().NotBeNull();
		}
	}
}
