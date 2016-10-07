using System;
using System.Collections.Generic;
using System.Linq;

namespace SixtenLabs.Simulacrum
{
  public sealed class ComponentManagerFactory : IComponentManagerFactory
  {
    public ComponentManagerFactory(IComponentFactory componentFactory)
    {
      ComponentFactory = componentFactory;
    }

    /// <summary>
    /// Create a component manager with all registered components.
    /// This should only be called by the Simulation class and only once.
    /// </summary>
    /// <returns></returns>
    public IComponentManager CreateComponentManager()
    {
      if (ComponentsHaveBeenRegistered)
      {
        throw new InvalidOperationException("The components have already been registered for this instance of the simulation. This method can only be called once.");
      }

      var components = ComponentFactory.GetAllComponents();

      int count = 0;

      foreach (var component in components)
      {
        component.AspectMask = count;
        RegisteredComponents.Add(component);
        count++;
      }

      ComponentsHaveBeenRegistered = true;

      return new ComponentManager(RegisteredComponents);
    }

    /// <summary>
    /// Create a component manager with only the registered components requested.
    /// This is used by simulators so they only have to be concerned about the components they care about.
    /// </summary>
    /// <param name="componentTypes"></param>
    /// <returns></returns>
    public IComponentManager CreateComponentManager(IList<Type> componentTypes)
    {
      var components = new List<IComponent>();

      foreach (var componentType in componentTypes)
      {
        var component = ComponentFactory.GetComponentByType(componentType);
        component.AspectMask = RegisteredComponents.Where(x => x.GetType() == componentType).FirstOrDefault().AspectMask;

        components.Add(component);
      }

      return new ComponentManager(components);
    }

    /// <summary>
    /// All registered components and their masks. This is set by Simulation when it starts up.
    /// It is used by the simulators when the component manager is created so that they have the 
    /// correct masks assigned so that the entity processors will recognize the correct components.
    /// 
    /// In other words the same component being used in different simulators must use the same mask.
    /// </summary>
    private IList<IComponent> RegisteredComponents { get; } = new List<IComponent>();

    private IComponentFactory ComponentFactory { get; }

    private bool ComponentsHaveBeenRegistered { get; set; }
  }
}
