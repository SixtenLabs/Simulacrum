using System;

namespace SixtenLabs.Simulacrum.Tests
{
  public class RenderProcessor : EntityProcessor
  {
    public RenderProcessor()
    {
    }

    protected override void SetupSystemProperties()
    {
      Order = 80;
      EntityProcessorType = EntityProcessorType.Render;
      Name = "Render System";
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
      var render = simulator.GetComponent<RenderComponent>();
      var transform = simulator.GetComponent<TransformComponent>();

      foreach (var handle in simulator.GetHandlesForProcessor(Aspect))
      {
        Console.BackgroundColor = render.Color[handle.Index];
        Console.Clear();

        var x = transform.X[handle.Index];
        var y = transform.Y[handle.Index];
        Console.SetCursorPosition(x, y);

        Console.Write(render.Text[handle.Index]);
      }
    }

    public override void Dispose()
    {
    }
  }
}
