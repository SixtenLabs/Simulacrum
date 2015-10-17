using System;
using System.Collections.Generic;

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
				component.AspectMask = Components.Count;
				Components.Add(component.GetType(), component);
			}
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

		public int AspectMask(Type componentType)
		{
			return Components[componentType].AspectMask;
		}

		/// <summary>
		/// TODO : revisit testing after finishing refactoring the Bucket.cs class.
		/// </summary>
		/// <param name="index"></param>
		public void DeleteComponentValues(int index)
		{
			foreach (var component in Components)
			{
				component.Value.Delete(index);
			}
		}

		/// <summary>
		/// The count of all the components.
		/// </summary>
		public int Count
		{
			get
			{
				return Components.Count;
			}
		}

		private IDictionary<Type, IComponent> Components { get; } = new Dictionary<Type, IComponent>();
	}
}
