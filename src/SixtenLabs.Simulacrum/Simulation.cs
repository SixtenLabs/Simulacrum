using System;
using System.Collections.Generic;
using System.Linq;

namespace SixtenLabs.Simulacrum
{
	/// <summary>
	/// A simulation manages multiple simlators.
	/// 
	/// Each contained simulator is independent but is updated by the same entity systems.
	/// </summary>
	public sealed class Simulation : ISimulation
	{
		public Simulation(IComponentManager componentManager, IEnumerable<IEntityProcessor> entitySystems, IEnumerable<ISimulator> simulators)
		{
			ComponentManager = componentManager;
			RegisterEntitySystems(entitySystems);
			RegisterSimulators(simulators);
		}

		private void RegisterEntitySystems(IEnumerable<IEntityProcessor> entitySystems)
		{
			foreach (var entitySystem in entitySystems.OrderBy(x => x.Order))
			{
				if (entitySystem.EntitySystemType == EntityProcessorType.Update)
				{
					RegisterUpdateSystem(entitySystem);
				}
				else if (entitySystem.EntitySystemType == EntityProcessorType.Render)
				{
					RegisterRenderSystem(entitySystem);
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="simulator"></param>
		private void RegisterSimulator(ISimulator simulator)
		{
			if (!Simulators.Contains(simulator))
			{
				Simulators.Add(simulator);
			}
		}

		private void RegisterUpdateSystem(IEntityProcessor entitySystem)
		{
			if (!UpdateSystems.Contains(entitySystem))
			{
				foreach (var componentType in entitySystem.RequiredComponentTypes)
				{
					var maskIndex = ComponentManager.AspectMask(componentType);
					entitySystem.Aspect.AddMask(maskIndex);
				}

				UpdateSystems.Add(entitySystem);
			}
		}

		private void RegisterRenderSystem(IEntityProcessor entitySystem)
		{
			if (!RenderSystems.Contains(entitySystem))
			{
				entitySystem.Aspect = new Aspect(ComponentManager.Count);

				foreach (var componentType in entitySystem.RequiredComponentTypes)
				{
					var maskIndex = ComponentManager.AspectMask(componentType);
					entitySystem.Aspect.AddMask(maskIndex);
				}

				RenderSystems.Add(entitySystem);
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
			foreach (var updateSystem in UpdateSystems)
			{
				updateSystem.Load();
			}

			foreach (var renderSystem in RenderSystems)
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
			foreach (var processor in UpdateSystems)
			{
				foreach (var simulator in ActiveSimulators)
				{
					processor.Process(simulator, tick);
				}
			}
		}

		public void Render(double tick)
		{
			foreach (var processor in RenderSystems)
			{
				foreach (var simulator in ActiveSimulators)
				{
					processor.Process(simulator, tick);
				}
			}
		}

		public void Dispose()
		{
			foreach (var updateSystem in UpdateSystems)
			{
				updateSystem.Dispose();
			}

			foreach (var renderSystem in RenderSystems)
			{
				renderSystem.Dispose();
			}

			foreach (var simulator in Simulators)
			{
				simulator.Dispose();
			}
		}

		private IList<IEntityProcessor> UpdateSystems { get; } = new List<IEntityProcessor>();

		private IList<IEntityProcessor> RenderSystems { get; } = new List<IEntityProcessor>();

		private IComponentManager ComponentManager { get; }

		private IList<ISimulator> Simulators { get; } = new List<ISimulator>();

		private IList<ISimulator> ActiveSimulators { get; } = new List<ISimulator>();
	}
}
