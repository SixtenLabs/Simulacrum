using System.Numerics;

namespace SixtenLabs.Simulacrum.ConsoleTest
{
	public class MovementProcessor : EntityProcessor
	{
		public MovementProcessor(IComponentManagerFactory componentManagerFactory)
      : base(componentManagerFactory)
    {
		}

		protected override void SetupSystemProperties()
		{
			Order = 20;
			EntityProcessorType = EntityProcessorType.Update;
			Name = "Movement System";
		}

		protected override void SetupComponentProperties()
		{
			TransformComponent = ComponentManager.GetComponent<TransformComponent>();
			VelocityComponent = ComponentManager.GetComponent<VelocityComponent>();
		}

		protected override void RegisterRequiredComponents()
		{
			var componentsTypes = new[]
			{
				typeof(TransformComponent),
				typeof(VelocityComponent)
			};

			RequiredComponentTypes.AddRange(componentsTypes);
		}

		protected override void RegisterOptionalComponents()
		{
		}

		public override void Process(ISimulator simulator, double tick)
		{
			foreach(var handle in simulator.GetHandlesForProcessor(Aspect))
			{
				if(VelocityComponent.MoveByX[handle.Index] != 0)
				{
					TransformComponent.X[handle.Index] += VelocityComponent.MoveByX[handle.Index];
					VelocityComponent.MoveByX[handle.Index] = 0;
				}

				if (VelocityComponent.MoveByY[handle.Index] != 0)
				{
					TransformComponent.Y[handle.Index] += VelocityComponent.MoveByY[handle.Index];
					VelocityComponent.MoveByY[handle.Index] = 0;
				}
			}
		}

		public override void Dispose()
		{
		}

		private TransformComponent TransformComponent { get; set; }

		private VelocityComponent VelocityComponent { get; set; }
	}
}
