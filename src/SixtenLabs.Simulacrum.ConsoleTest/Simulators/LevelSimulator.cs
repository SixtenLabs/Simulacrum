using System;

namespace SixtenLabs.Simulacrum.ConsoleTest
{
	public class LevelSimulator : Simulator
	{
		public LevelSimulator(IComponentManagerFactory componentManagerFactory, IConsole console)
			: base(componentManagerFactory)
		{
			Console = console;
		}

		protected override void SetupProperties()
		{
			Order = 1;
			Name = "Level 1";
		}

		public override void Load()
		{
			Console.SetFullScreen();

			CreatePlayer();
		}

		private void CreatePlayer()
		{
			var entity = CreateEntity();

			var rendercomponent = GetComponent<RenderComponent>();

			rendercomponent.Text[entity.Index] = "%";
			rendercomponent.Color[entity.Index] = ConsoleColor.Green;

			entity.Aspect.AddMask(rendercomponent.AspectMask);

			var transformComponent = GetComponent<TransformComponent>();

			transformComponent.X[entity.Index] = 10;
			transformComponent.Y[entity.Index] = 10;

			entity.Aspect.AddMask(transformComponent.AspectMask);


			var velocityComponent = GetComponent<VelocityComponent>();

			entity.Aspect.AddMask(velocityComponent.AspectMask);
		}

		private IConsole Console { get; }
	}
}
