using System.Collections.Generic;

using Xunit;
using FluentAssertions;

using SixtenLabs.Simulacrum.SampleImplementation;

namespace SixtenLabs.Simulacrum.Tests
{
	public class ComponentManagerFactoryTests
	{

		private ComponentManagerFactory NewSubjectUnderTest()
		{
			var components = new List<IComponent>();

			components.Add(new TransformComponent());
			components.Add(new VelocityComponent());

			var factory = new ComponentManagerFactory(components);

			return factory;
		}

		[Fact]
		public void CreateComponentManager()
		{
			var subject = NewSubjectUnderTest();

			var actual = subject.CreateComponentManager();

			actual.Should().NotBeNull();
		}
	}
}
