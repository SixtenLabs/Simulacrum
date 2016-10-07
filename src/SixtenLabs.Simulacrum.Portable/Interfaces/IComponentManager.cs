using System;

namespace SixtenLabs.Simulacrum
{
  public interface IComponentManager
  {
    int AspectMask(Type componentType);

    /// <summary>
    /// The count of all the components.
    /// </summary>
    int Count { get; }

    T GetComponent<T>() where T : class, IComponent;

    void DeleteComponentValues(int index);
  }
}
