using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Simulacrum
{
	public class ComponentManager : IComponentManager
	{
		public ComponentManager(IEnumerable<IComponent> components)
		{
			RegisterComponents(components);
		}

		private void RegisterComponents(IEnumerable<IComponent> components)
		{
			foreach (var component in components)
			{
				var componentType = component.GetType();
        var mask = RegisterComponentType(componentType);
				component.ComponentTypeMask = mask;
				Components.Add(componentType, component);
			}
		}

		/// <summary>
		/// Each component has to be registered.
		/// </summary>
		/// <param name="componentType"></param>
		/// <returns>ComponentTypeMask bit (if -1 is returned it is not a valid mask (means we already added it)</returns>
		private int RegisterComponentType(Type componentType)
		{
			int mask = -1;

			if (!Registry.Keys.Contains(componentType))
			{
				mask = Registry.Count + 1;
				Registry.Add(componentType, mask);
			}
			else
			{
				mask = Registry[componentType];
			}

			return mask;
		}

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

		public int ComponentMask(Type componentType)
		{
			return Registry[componentType];
		}

		public void DeleteComponentValues(int index)
		{
			foreach (var component in Components)
			{
				component.Value.Delete(index);
			}
		}

		public int ComponentCount
		{
			get
			{
				return Registry.Count;
			}
		}

		private IDictionary<Type, int> Registry { get; } = new Dictionary<Type, int>();

		private IDictionary<Type, IComponent> Components { get; } = new Dictionary<Type, IComponent>();
	}
}
