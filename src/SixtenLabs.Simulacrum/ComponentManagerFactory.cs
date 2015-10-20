using System.Collections.Generic;
using System.Linq;

namespace SixtenLabs.Simulacrum
{
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
