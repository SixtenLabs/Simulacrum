using System;

namespace SixtenLabs.Simulacrum.Tests
{
  public class InputProcessor : EntityProcessor
  {
    public InputProcessor()
    {
    }

    protected override void SetupSystemProperties()
    {
      Order = 10;
      EntityProcessorType = EntityProcessorType.Update;
      Name = "Input System";
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
      var velocity = simulator.GetComponent<VelocityComponent>();

      foreach (var handle in simulator.GetHandlesForProcessor(Aspect))
      {
        if (Console.KeyAvailable)
        {
          var key = Console.ReadKey(true);

          switch (key.Key)
          {
            case ConsoleKey.UpArrow:
              velocity.MoveByX[handle.Index] = 1;
              break;
            case ConsoleKey.DownArrow:
              velocity.MoveByX[handle.Index] = -1;
              break;
            case ConsoleKey.LeftArrow:
              velocity.MoveByY[handle.Index] = -1;
              break;
            case ConsoleKey.RightArrow:
              velocity.MoveByY[handle.Index] = 1;
              break;
          }
        }
      }
    }

    public override void Dispose()
    {
    }
  }
}
