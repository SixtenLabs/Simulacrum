using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Simulacrum.SampleImplementation
{
	public class RenderComponent : Component
	{
		public override void Delete(int index)
		{
			Text.Delete(index);
			Color.Delete(index);
		}

		public Bag<string> Text { get; } = new Bag<string>();

		public Bag<ConsoleColor> Color { get; } = new Bag<ConsoleColor>();
	}
}
