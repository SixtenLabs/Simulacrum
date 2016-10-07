using SimpleInjector;
using System;
using System.Collections.Generic;

namespace SixtenLabs.Simulacrum.ConsoleTest
{
  public class ComponentFactory : IComponentFactory
  {
    public ComponentFactory(Container simpleContainer)
    {
      SimpleContainer = simpleContainer;
    }

    public IComponent GetComponentByType(Type componentType)
    {
      return SimpleContainer.GetInstance(componentType) as IComponent;
    }

    public IEnumerable<IComponent> GetAllComponents()
    {
      return SimpleContainer.GetAllInstances<IComponent>();
    }

    private Container SimpleContainer { get; }
  }
}
