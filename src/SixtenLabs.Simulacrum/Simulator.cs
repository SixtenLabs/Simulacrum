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
		public Simulator(IComponentManager componentManager)
		{
			SetupProperties();
		}

		protected abstract void SetupProperties();

		public abstract void Load();

		/// <summary>
		/// Get component for a specific Component Type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetComponent<T>() where T : class, IComponent
		{
			return ComponentManager.GetComponent<T>();
		}

		public EntityHandle CreateEntity()
		{
			var handle = new EntityHandle(Guid.NewGuid(), ComponentManager.Count);

			Handles.Add(handle);

			return handle;
		}

		public void DeleteEntity(EntityHandle handle)
		{
			Handles.Remove(handle);
			EntityHandle.UsedIndexPool.Enqueue(handle.Index);
			ComponentManager.DeleteComponentValues(handle.Index);
		}

		public IList<EntityHandle> GetHandlesForProcessor(Aspect aspect)
		{
			return Handles.Where(x => aspect.IsSubsetOf(x.Aspect)).ToList();
		}

		public virtual void Dispose()
		{
		}

		private IList<EntityHandle> Handles { get; } = new List<EntityHandle>();

		public IComponentManager ComponentManager { get; }

		public int Order { get; set; }

		public string Name { get; set; }
	}
}
