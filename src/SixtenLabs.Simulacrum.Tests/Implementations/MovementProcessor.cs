namespace SixtenLabs.Simulacrum.Tests
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
      var transform = simulator.GetComponent<TransformComponent>();
      var velocity = simulator.GetComponent<VelocityComponent>();

      foreach (var handle in simulator.GetHandlesForProcessor(Aspect))
      {
        if (velocity.MoveByX[handle.Index] != 0)
        {
          transform.X[handle.Index] += velocity.MoveByX[handle.Index];
          velocity.MoveByX[handle.Index] = 0;
        }

        if (velocity.MoveByY[handle.Index] != 0)
        {
          transform.Y[handle.Index] += velocity.MoveByY[handle.Index];
          velocity.MoveByY[handle.Index] = 0;
        }
      }
    }

    public override void Dispose()
    {
    }
  }
}
