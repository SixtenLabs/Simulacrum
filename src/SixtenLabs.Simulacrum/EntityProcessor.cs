using System;
using System.Collections.Generic;

namespace SixtenLabs.Simulacrum
{
	public abstract class EntityProcessor : IEntityProcessor
	{
		public EntityProcessor(IEnumerable<IComponent> components)
		{
			RegisterRequiredComponents();
			RegisterOptionalComponents();

			SetComponents(components);

			SetupSystemProperties();
			SetupComponentProperties();
		}

		private void SetComponents(IEnumerable<IComponent> components)
		{
			foreach (var component in components)
			{
				Components.Add(component.GetType(), component);
			}
		}

		protected abstract void SetupSystemProperties();

		protected abstract void SetupComponentProperties();

		protected abstract void RegisterRequiredComponents();

		protected abstract void RegisterOptionalComponents();

		public abstract void Dispose();

		public abstract void Process(ISimulator space, double time);

		public virtual void Load()
		{
			Console.WriteLine(string.Format("{0} - {1} Loaded.", Order, Name));
		}

		/// <summary>
		/// This is a collection of all registered components in the entire simulation.
		/// While each system will usually only access a few components they have access to any component.
		/// 
		/// </summary>
		protected IDictionary<Type, IComponent> Components { get; } = new Dictionary<Type, IComponent>();

		public BitSet ComponentTypeMask { get; } = new BitSet();

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

		public EntityProcessorType EntitySystemType { get; protected set; }

		public string Name { get; protected set; }
	}
}
