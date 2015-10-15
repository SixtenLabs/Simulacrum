using System;
using System.Collections.Generic;
using System.Linq;

namespace SixtenLabs.Simulacrum
{
	/// <summary>
	/// A simulator handles a distinct group of entities
	/// This simulator has no interaction with any other simulator.
	/// </summary>
	public abstract class Simulator : ISimulator
	{
		public Simulator(IEnumerable<IComponent> components)
		{
			AddComponents(components);
			SetupProperties();
		}

		protected abstract void SetupProperties();

		private void AddComponents(IEnumerable<IComponent> components)
		{
			foreach (var component in components)
			{
				Components.Add(component.GetType(), component);
			}
		}

		public abstract void Load();

		/// <summary>
		/// Get component for a specific Component Type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetComponent<T>() where T : class, IComponent
		{
			var component = Components[typeof(T)];

			return component as T;
		}

		public EntityHandle CreateEntity()
		{
			var handle = new EntityHandle(Guid.NewGuid());

			Handles.Add(handle);

			return handle;
		}

		public void DeleteEntity(EntityHandle handle)
		{
			Handles.Remove(handle);
			EntityHandle.UsedIndexPool.Enqueue(handle.Index);

			foreach (var component in Components)
			{
				component.Value.Delete(handle.Index);
			}
		}

		public IList<EntityHandle> GetHandlesForProcessor(BitSet processorComponentMask)
		{
			return Handles.Where(x => processorComponentMask.IsSubsetOf(x.ComponentTypesMask)).ToList();
		}

		public virtual void Dispose()
		{
		}

		private IList<EntityHandle> Handles { get; } = new List<EntityHandle>();

		public IDictionary<Type, IComponent> Components { get; } = new Dictionary<Type, IComponent>();

		public int Order { get; set; }

		public string Name { get; set; }
	}
}
