using System;
using System.Collections.Generic;

namespace SixtenLabs.Simulacrum
{
  public interface IComponentManagerFactory
  {
    IComponentManager CreateComponentManager();

    IComponentManager CreateComponentManager(IList<Type> componentTypes);
  }
}
