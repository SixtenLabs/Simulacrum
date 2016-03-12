using SimpleInjector;
using System;

namespace SixtenLabs.Simulacrum.ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap();

			var game = Container.GetInstance<GameWindow>();

			game.Run();
		}

		private static void RegisterComponents()
		{
			Container.Register<TransformComponent>();
			Container.Register<VelocityComponent>();
			Container.Register<RenderComponent>();
			Container.RegisterCollection<IComponent>(new[] { typeof(TransformComponent), typeof(VelocityComponent), typeof(RenderComponent) });
		}

		private static void RegisterProcessors()
		{
			Container.RegisterSingleton<MovementProcessor>();
			Container.RegisterSingleton<RenderProcessor>();
			Container.RegisterSingleton<InputProcessor>();
			Container.RegisterCollection<IEntityProcessor>(new[] { typeof(MovementProcessor), typeof(RenderProcessor), typeof(InputProcessor) });
		}

		private static void RegisterSimulators()
		{
			Container.RegisterSingleton<LevelSimulator>();
			Container.RegisterCollection<ISimulator>(new[] { typeof(LevelSimulator) });
		}

		private static void Bootstrap()
		{
			Console.WriteLine("Bootstrap Started");

			Container.RegisterSingleton<ISimulation, Simulation>();
			Container.RegisterSingleton<IComponentManagerFactory, ComponentManagerFactory>();
			Container.RegisterSingleton<IConsole, WindowsConsole>();

			Container.RegisterSingleton<GameWindow>();

			RegisterComponents();
			RegisterProcessors();
			RegisterSimulators();

			Container.Verify();

			Console.WriteLine("Bootstrap Verified");
		}

		private static Container Container { get; } = new Container();
	}
}
