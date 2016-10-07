using System;
using System.Collections.Generic;

namespace SixtenLabs.Simulacrum
{
  /// <summary>
	/// This needs to be implemented at the client level where the container lives
	/// using the IOC container to return instances of the components.
	/// 
	/// This is consumed by the ComponentManagerFactory and is used to provide new instances
	/// to each simulator based on their registrations of components types. 
	/// 
	/// The simulation itself is given its own instances of all components.
	/// </summary>
	public interface IComponentFactory
  {
    IComponent GetComponentByType(Type componentType);

    IEnumerable<IComponent> GetAllComponents();
  }
}
