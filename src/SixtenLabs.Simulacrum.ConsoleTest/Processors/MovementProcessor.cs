using System.Numerics;

namespace SixtenLabs.Simulacrum.ConsoleTest
{
	public class MovementProcessor : EntityProcessor
	{
		public MovementProcessor()
    {
		}

		protected override void SetupSystemProperties()
		{
			Order = 20;
			EntityProcessorType = EntityProcessorType.Update;
			Name = "Movement System";
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
      var transformComponent = simulator.GetComponent<TransformComponent>();
      var velocityComponent = simulator.GetComponent<VelocityComponent>();

      foreach (var handle in simulator.GetHandlesForProcessor(Aspect))
			{
				if(velocityComponent.MoveByX[handle.Index] != 0)
				{
          transformComponent.X[handle.Index] += velocityComponent.MoveByX[handle.Index];
          velocityComponent.MoveByX[handle.Index] = 0;
				}

				if (velocityComponent.MoveByY[handle.Index] != 0)
				{
          transformComponent.Y[handle.Index] += velocityComponent.MoveByY[handle.Index];
          velocityComponent.MoveByY[handle.Index] = 0;
				}
			}
		}

		public override void Dispose()
		{
		}
	}
}
