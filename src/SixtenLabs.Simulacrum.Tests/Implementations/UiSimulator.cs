using System;

namespace SixtenLabs.Simulacrum.Tests
{
  public class UiSimulator : Simulator
  {
    public UiSimulator(IComponentManagerFactory componentManagerFactory)
      : base(componentManagerFactory)
    {
    }

    protected override void SetupProperties()
    {
      Order = 1;
      Name = "Level 1";
    }

    protected override void RegisterComponentTypes()
    {
      RegisteredComponentTypes.Add(typeof(UiComponent));
    }

    public override void Load()
    {
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
  }
}
