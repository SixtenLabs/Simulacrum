using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using FluentAssertions;
using NSubstitute;
using SixtenLabs.Simulacrum.SampleImplementation.Simulators;
using SixtenLabs.Simulacrum.SampleImplementation;

namespace SixtenLabs.Simulacrum.Tests
{
	public class SimulatorTests
	{
		private IComponentManagerFactory ComponentManagerFactory { get; set; }

		public SimulatorTests()
		{
			ComponentManagerFactory = Substitute.For<IComponentManagerFactory>();
			ComponentManagerFactory.CreateComponentManager().Returns(ComponentManager());
		}

		private LevelSimulator NewSubjectUnderTest()
		{
			var simulator = new LevelSimulator(ComponentManagerFactory);

			return simulator;
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
		public void Constructor_Order_IsSet()
		{
			var subject = NewSubjectUnderTest();

			subject.Order.Should().Be(1);
		}

		[Fact]
		public void Constructor_Name_IsSet()
		{
			var subject = NewSubjectUnderTest();

			subject.Name.Should().Be("Planet");
		}

		[Fact]
		public void Load_x_x()
		{
			var subject = NewSubjectUnderTest();

			subject.Load();

			// Do not have anything to test yet. Maybe when I add logging support.
		}

		[Fact]
		public void GetCompoennt_Returns_Component()
		{
			var subject = NewSubjectUnderTest();

			var actual = subject.GetComponent<TransformComponent>();

			actual.Should().NotBeNull();
		}

		[Fact]
		public void CreateEntity_x_x()
		{
			var subject = NewSubjectUnderTest();

			var actual = subject.CreateEntity();

			actual.Should().NotBeNull();
			actual.Index.Should().Be(0);
		}

		[Fact]
		public void DeleteEntity_WhenNextEntityIsCreated_ResusesIndex()
		{
			var subject = NewSubjectUnderTest();

			var actual = subject.CreateEntity();

			subject.DeleteEntity(actual);

			var newEntity = subject.CreateEntity();

			newEntity.Index.Should().Be(actual.Index);
		}

		[Fact]
		public void Dispose_x_x()
		{
			var subject = NewSubjectUnderTest();

			subject.Dispose();

			// Do not have anything to test yet. Maybe when I add logging support.
		}

		[Fact]
		public void GetHandlesForProcessor_x_x()
		{
			var subject = NewSubjectUnderTest();
			

			var entity1 = subject.CreateEntity();
			entity1.Aspect.AddMask(0);
			entity1.Aspect.AddMask(1);

			var entity2 = subject.CreateEntity();
			entity2.Aspect.AddMask(0);
			entity2.Aspect.AddMask(1);

			var entity3 = subject.CreateEntity();
			entity3.Aspect.AddMask(1);
			
			var aspect = new Aspect(2);
			aspect.AddMask(0);
			aspect.AddMask(1);

			var actual = subject.GetHandlesForProcessor(aspect);

			actual.Count.Should().Be(2);
		}
	}
}
