using System.Collections.Generic;
using System.Linq;

namespace SixtenLabs.Simulacrum
{
	/// <summary>
	/// This exists to ensure that each simulator gets it own component manager with its own set
	/// of components.
	/// </summary>
	public class ComponentManagerFactory : IComponentManagerFactory
	{
		public ComponentManagerFactory(IEnumerable<IComponent> components)
		{
			Components = components.ToList();
		}

		public IComponentManager CreateComponentManager()
		{
			return new ComponentManager(Components);
		}

		private IList<IComponent> Components { get; }
	}
}
