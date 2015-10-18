using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Simulacrum.Tests.Components
{
	public sealed class TransformComponent : Component
	{
		public override void Delete(int index)
		{
			Position.Remove(index);
			Orientation.Remove(index);
			Scale.Remove(index);
		}

		public Bag<Vector3> Position { get; } = new Bag<Vector3>();

		public Bag<Quaternion> Orientation { get; } = new Bag<Quaternion>();

		public Bag<Vector3> Scale { get; } = new Bag<Vector3>();
	}
}
