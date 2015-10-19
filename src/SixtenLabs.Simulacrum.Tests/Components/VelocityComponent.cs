using System.Collections.Concurrent;
using System.Numerics;

namespace SixtenLabs.Simulacrum.Tests.Components
{
	public class VelocityComponent : Component
	{
		public VelocityComponent()
		{
		}

		public override void Delete(int index)
		{
			//RunSpeed.Remove(index);
			//TurnSpeed.Remove(index);
			//CurrentMoveBy.Remove(index);
			//CurrentRotationSpeed.Remove(index);
		}

		public ConcurrentBag<float> RunSpeed { get; } = new ConcurrentBag<float>();

		public ConcurrentBag<float> TurnSpeed { get; } = new ConcurrentBag<float>();

		public ConcurrentBag<Vector3> CurrentMoveBy { get; } = new ConcurrentBag<Vector3>();

		public ConcurrentBag<float> CurrentRotationSpeed { get; } = new ConcurrentBag<float>();
	}
}
