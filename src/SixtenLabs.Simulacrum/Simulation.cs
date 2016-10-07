using System.Collections.Generic;
using System.Linq;

namespace SixtenLabs.Simulacrum
{
  /// <summary>
	/// A simulation manages multiple simulators.
	/// 
	/// Each contained simulator is independent but is updated by the same entity systems.
	/// </summary>
	public sealed class Simulation : ISimulation
  {
    public Simulation(IComponentManagerFactory componentManagerFactory, IEnumerable<IEntityProcessor> entityProcessors, IEnumerable<ISimulator> simulators)
    {
      ComponentManager = componentManagerFactory.CreateComponentManager();
      RegisterEntityProcessors(entityProcessors);
      RegisterSimulators(simulators);
    }

    private void RegisterEntityProcessors(IEnumerable<IEntityProcessor> processors)
    {
      foreach (var processor in processors.OrderBy(x => x.Order))
      {
        if (processor.EntityProcessorType == EntityProcessorType.Update)
        {
          RegisterUpdateProcessor(processor);
        }
        else if (processor.EntityProcessorType == EntityProcessorType.Render)
        {
          RegisterRenderProcessor(processor);
        }
      }
    }

    private void RegisterSimulators(IEnumerable<ISimulator> simulators)
    {
      foreach (var simulator in simulators.OrderBy(x => x.Order))
      {
        RegisterSimulator(simulator);
      }
    }

    private void RegisterSimulator(ISimulator simulator)
    {
      if (!Simulators.Contains(simulator))
      {
        Simulators.Add(simulator);
      }
    }

    private void RegisterUpdateProcessor(IEntityProcessor processor)
    {
      if (!UpdateProcessors.Contains(processor))
      {
        processor.Aspect = new Aspect(ComponentManager.Count);

        foreach (var componentType in processor.RequiredComponentTypes)
        {
          var maskIndex = ComponentManager.AspectMask(componentType);
          processor.Aspect.AddMask(maskIndex);
        }

        UpdateProcessors.Add(processor);
      }
    }

    private void RegisterRenderProcessor(IEntityProcessor processor)
    {
      if (!RenderProcessors.Contains(processor))
      {
        processor.Aspect = new Aspect(ComponentManager.Count);

        foreach (var componentType in processor.RequiredComponentTypes)
        {
          var maskIndex = ComponentManager.AspectMask(componentType);
          processor.Aspect.AddMask(maskIndex);
        }

        RenderProcessors.Add(processor);
      }
    }

    public void ActivateSimulator(string name)
    {
      var simulator = GetSimulatorByName(name);

      if (simulator != null && !ActiveSimulators.Contains(simulator))
      {
        ActiveSimulators.Add(simulator);
      }
    }

    public void DeactivateSimulator(string name)
    {
      var simulator = GetSimulatorByName(name);

      if (simulator != null && ActiveSimulators.Contains(simulator))
      {
        ActiveSimulators.Remove(simulator);
      }
    }

    public void Load()
    {
      foreach (var updateSystem in UpdateProcessors)
      {
        updateSystem.Load();
      }

      foreach (var renderSystem in RenderProcessors)
      {
        renderSystem.Load();
      }

      foreach (var simulator in Simulators)
      {
        simulator.Load();
      }
    }

    public ISimulator GetSimulatorByName(string name)
    {
      return Simulators.Where(x => x.Name == name).FirstOrDefault();
    }

    public void Update(double tick)
    {
      foreach (var processor in UpdateProcessors)
      {
        foreach (var simulator in ActiveSimulators)
        {
          processor.Process(simulator, tick);
        }
      }
    }

    public void Render(double tick)
    {
      foreach (var processor in RenderProcessors)
      {
        foreach (var simulator in ActiveSimulators)
        {
          processor.Process(simulator, tick);
        }
      }
    }

    public void Dispose()
    {
      foreach (var updateSystem in UpdateProcessors)
      {
        updateSystem.Dispose();
      }

      foreach (var renderSystem in RenderProcessors)
      {
        renderSystem.Dispose();
      }

      foreach (var simulator in Simulators)
      {
        simulator.Dispose();
      }
    }

    private IList<IEntityProcessor> UpdateProcessors { get; } = new List<IEntityProcessor>();

    private IList<IEntityProcessor> RenderProcessors { get; } = new List<IEntityProcessor>();

    private IComponentManager ComponentManager { get; }

    private IList<ISimulator> Simulators { get; } = new List<ISimulator>();

    private IList<ISimulator> ActiveSimulators { get; } = new List<ISimulator>();
  }
}
