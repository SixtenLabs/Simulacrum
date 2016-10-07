using System;
using System.Collections.Generic;

namespace SixtenLabs.Simulacrum
{
  public abstract class EntityProcessor : IEntityProcessor
  {
    public EntityProcessor()
    {
      RegisterRequiredComponents();
      RegisterOptionalComponents();

      SetupSystemProperties();
    }

    protected abstract void SetupSystemProperties();

    protected abstract void RegisterRequiredComponents();

    protected abstract void RegisterOptionalComponents();

    public abstract void Dispose();

    public abstract void Process(ISimulator space, double time);

    public virtual void Load()
    {
    }

    public Aspect Aspect { get; set; }

    /// <summary>
    /// These component types are required by this system and 
    /// compose the ComponentTypeMask
    /// </summary>
    public List<Type> RequiredComponentTypes { get; } = new List<Type>();

    /// <summary>
    /// These are components that this system might use but are not required
    /// and are not part of the ComponentTypeMask
    /// </summary>
    public List<Type> OptionalComponentTypes { get; } = new List<Type>();

    public int Order { get; protected set; }

    public EntityProcessorType EntityProcessorType { get; protected set; }

    public string Name { get; protected set; }
  }
}
