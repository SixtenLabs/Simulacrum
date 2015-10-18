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
			RunSpeed.Remove(index);
			TurnSpeed.Remove(index);
			CurrentMoveBy.Remove(index);
			CurrentRotationSpeed.Remove(index);
		}

		public Bag<float> RunSpeed { get; } = new Bag<float>();

		public Bag<float> TurnSpeed { get; } = new Bag<float>();

		public Bag<Vector3> CurrentMoveBy { get; } = new Bag<Vector3>();

		public Bag<float> CurrentRotationSpeed { get; } = new Bag<float>();
	}
}
