using System.Collections.Generic;

using Xunit;
using FluentAssertions;
using NSubstitute;

using SixtenLabs.Simulacrum.ConsoleTest;

namespace SixtenLabs.Simulacrum.Tests
{
	public class EntityProcessorTests
	{
		private IComponentManagerFactory ComponentManagerFactory { get; set; }

		public EntityProcessorTests()
		{
			ComponentManagerFactory = Substitute.For<IComponentManagerFactory>();
			ComponentManagerFactory.CreateComponentManager().Returns(ComponentManager());
		}

		private MovementProcessor NewSubjectUnderTest()
		{
			var processor = new MovementProcessor(ComponentManagerFactory);

			return processor;
		}

		private ComponentManager ComponentManager()
		{
			var components = new List<IComponent>();

			components.Add(new TransformComponent());
			components.Add(new VelocityComponent());

			var manager = new ComponentManager(components);

			return manager;
		}

		[Fact]
		public void Load_x_x()
		{
			var subject = NewSubjectUnderTest();

			subject.Load();

			// Do not have anything to test yet. Maybe when I add logging support.
		}

		[Fact]
		public void Dispose_x_x()
		{
			var subject = NewSubjectUnderTest();

			subject.Dispose();

			// Do not have anything to test yet. Maybe when I add logging support.
		}

		//[Fact]
		//public void Process_x_x()
		//{
		//	var subject = NewSubjectUnderTest();

		//	subject.Process(null, 1.0);

		//	// Do not have anything to test yet. Maybe when I add logging support.
		//}

		[Fact]
		public void Constructor_Order_IsSet()
		{
			var subject = NewSubjectUnderTest();

			subject.Load();

			subject.Order.Should().Be(20);
		}

		[Fact]
		public void Constructor_EntitySystemType_IsSet()
		{
			var subject = NewSubjectUnderTest();

			subject.Load();

			subject.EntityProcessorType.Should().Be(EntityProcessorType.Update);
		}

		[Fact]
		public void Constructor_Aspect_IsNotSet()
		{
			var subject = NewSubjectUnderTest();

			subject.Load();

			subject.Aspect.Should().BeNull();
		}
	}
}
