using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using FluentAssertions;
using NSubstitute;
using SixtenLabs.Simulacrum.SampleImplementation;

namespace SixtenLabs.Simulacrum.Tests
{
	public class SimulationTests
	{
		private IComponentManagerFactory ComponentManagerFactory { get; set; }

		private IList<IEntityProcessor> EntityProcessors { get; } = new List<IEntityProcessor>();

		private IList<ISimulator> Simulators { get; } = new List<ISimulator>();

		private ISimulator WorldSimulator { get; set; }

		private ISimulator UiSimulator { get; set; }

		private IEntityProcessor MovememtProcessor { get; set; }

		private IEntityProcessor RenderProcessor { get; set; }

		public SimulationTests()
		{
			ComponentManagerFactory = Substitute.For<IComponentManagerFactory>();
			ComponentManagerFactory.CreateComponentManager().Returns(ComponentManager());

			MovememtProcessor = Substitute.For<IEntityProcessor>();
			MovememtProcessor.EntityProcessorType.Returns(EntityProcessorType.Update);
			MovememtProcessor.RequiredComponentTypes.Returns(new Type[] { typeof(TransformComponent), typeof(VelocityComponent) }.ToList());

			RenderProcessor = Substitute.For<IEntityProcessor>();
			RenderProcessor.EntityProcessorType.Returns(EntityProcessorType.Render);
			RenderProcessor.RequiredComponentTypes.Returns(new Type[] { typeof(TransformComponent) }.ToList());

			EntityProcessors.Add(MovememtProcessor);
			EntityProcessors.Add(RenderProcessor);

			WorldSimulator = Substitute.For<ISimulator>();
			WorldSimulator.Name.Returns("WorldSimulator");

			UiSimulator = Substitute.For<ISimulator>();
			UiSimulator.Name.Returns("UiSimulator");

			Simulators.Add(WorldSimulator);
			Simulators.Add(UiSimulator);
		}

		private ComponentManager ComponentManager()
		{
			var components = new List<IComponent>();

			components.Add(new TransformComponent());
			components.Add(new VelocityComponent());

			var manager = new ComponentManager(components);

			return manager;
		}

		private Simulation NewSubjectUnderTest()
		{
			var simulation = new Simulation(ComponentManagerFactory, EntityProcessors, Simulators);

			return simulation;
		}

		[Fact]
		public void Load_CallsLoad_OnAllProcessorsAndSimulators()
		{
			var subject = NewSubjectUnderTest();

			subject.Load();

			MovememtProcessor.Received(1).Load();
			RenderProcessor.Received(1).Load();

			WorldSimulator.Received(1).Load();
			UiSimulator.Received(1).Load();
		}

		[Fact]
		public void ActivateSimulator_X_X()
		{
			var subject = NewSubjectUnderTest();

			subject.ActivateSimulator("WorldSimulator");
			
			// Do not have a clear way to test success at the moment.
		}

		[Fact]
		public void DeactivateSimulator_X_X()
		{
			var subject = NewSubjectUnderTest();

			subject.ActivateSimulator("WorldSimulator");

			subject.DeactivateSimulator("WorldSimulator");

			// Do not have a clear way to test success at the moment.
		}

		[Fact]
		public void Dispose_CallsDispose_OnAllProcessorsAndSimulators()
		{
			var subject = NewSubjectUnderTest();

			subject.Dispose();

			MovememtProcessor.Received(1).Dispose();
			RenderProcessor.Received(1).Dispose();

			WorldSimulator.Received(1).Dispose();
			UiSimulator.Received(1).Dispose();
		}

		[Fact]
		public void Update_XX_XX()
		{
			var subject = NewSubjectUnderTest();

			subject.ActivateSimulator("WorldSimulator");

			subject.Update(1.0);

			MovememtProcessor.Received(1).Process(Arg.Any<ISimulator>(), 1.0);
			RenderProcessor.DidNotReceive().Process(Arg.Any<ISimulator>(), 1.0);
		}

		[Fact]
		public void Render_XX_XX()
		{
			var subject = NewSubjectUnderTest();

			subject.ActivateSimulator("WorldSimulator");

			subject.Render(1.0);

			MovememtProcessor.DidNotReceive().Process(Arg.Any<ISimulator>(), 1.0);
			RenderProcessor.Received(1).Process(Arg.Any<ISimulator>(), 1.0);
		}
	}
}
