using System;

namespace SixtenLabs.Simulacrum.ConsoleTest
{
	public class InputProcessor : EntityProcessor
	{
		public InputProcessor(IComponentManagerFactory componentManagerFactory)
      : base(componentManagerFactory)
    {
		}

		protected override void SetupSystemProperties()
		{
			Order = 10;
			EntityProcessorType = EntityProcessorType.Update;
			Name = "Input System";
		}

		protected override void SetupComponentProperties()
		{
			VelocityComponent = ComponentManager.GetComponent<VelocityComponent>();
		}

		protected override void RegisterRequiredComponents()
		{
			var componentsTypes = new[]
			{
				typeof(TransformComponent),
				typeof(RenderComponent)
			};

			RequiredComponentTypes.AddRange(componentsTypes);
		}

		protected override void RegisterOptionalComponents()
		{
		}

		public override void Process(ISimulator simulator, double tick)
		{
			foreach (var handle in simulator.GetHandlesForProcessor(Aspect))
			{
				if(Console.KeyAvailable)
				{
					var key = Console.ReadKey(true);

					switch(key.Key)
					{
						case ConsoleKey.UpArrow:
							VelocityComponent.MoveByX[handle.Index] = 1;
							break;
						case ConsoleKey.DownArrow:
							VelocityComponent.MoveByX[handle.Index] = -1;
							break;
						case ConsoleKey.LeftArrow:
							VelocityComponent.MoveByY[handle.Index] = -1;
							break;
						case ConsoleKey.RightArrow:
							VelocityComponent.MoveByY[handle.Index] = 1;
							break;
					}
				}
			}
		}

		public override void Dispose()
		{
		}

		private VelocityComponent VelocityComponent { get; set; }
	}
}
