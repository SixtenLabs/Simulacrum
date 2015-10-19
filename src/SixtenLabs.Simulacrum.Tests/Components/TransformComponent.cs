using System;
using System.Collections.Concurrent;
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
			//Position[0].cle.Remove(index);
			//Orientation.Remove(index);
			//Scale.Remove(index);
		}

		public ConcurrentBag<Vector3> Position { get; } = new ConcurrentBag<Vector3>();

		public ConcurrentBag<Quaternion> Orientation { get; } = new ConcurrentBag<Quaternion>();

		public ConcurrentBag<Vector3> Scale { get; } = new ConcurrentBag<Vector3>();
	}
}
