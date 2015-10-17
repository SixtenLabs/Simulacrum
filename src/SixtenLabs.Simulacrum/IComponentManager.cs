using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Simulacrum
{
	public interface IComponentManager
	{
		int ComponentMask(Type componentType);

		int ComponentCount { get; }

		T GetComponent<T>() where T : class, IComponent;

		void DeleteComponentValues(int index);
  }
}
