using SimpleInjector;
using SixtenLabs.Simulacrum.SampleImplementation;
using SixtenLabs.Simulacrum.SampleImplementation.Processors;
using SixtenLabs.Simulacrum.SampleImplementation.Simulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Simulacrum.ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap();
		}

		private static void RegisterComponents()
		{
			Container.Register<TransformComponent>();
			Container.Register<VelocityComponent>();

			Container.RegisterCollection<IComponent>(new[] { typeof(TransformComponent), typeof(VelocityComponent) });
		}

		private static void RegisterProcessors()
		{
			Container.RegisterSingleton<MovementProcessor>();

			Container.RegisterCollection<IEntityProcessor>(new[] { typeof(MovementProcessor) });
		}

		private static void RegisterSimulators()
		{
			Container.RegisterSingleton<LevelSimulator>();

			Container.RegisterCollection<ISimulator>(new[] { typeof(LevelSimulator) });
		}

		private static void Bootstrap()
		{
			Console.WriteLine("Bootstrap Started");

			Container = new Container();

			Container.RegisterSingleton<ISimulation, Simulation>();
			//Container.Register<IComponentManager, ComponentManager>();
			Container.RegisterSingleton<IComponentManagerFactory, ComponentManagerFactory>();
			//Container.RegisterSingleton<ILog, ConsoleLog>();

			RegisterComponents();
			RegisterProcessors();
			RegisterSimulators();

			Container.Verify();

			Console.WriteLine("Bootstrap Verified");

			Console.ReadLine();
		}

		private static Container Container;
	}
}
