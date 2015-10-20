using System.Numerics;

namespace SixtenLabs.Simulacrum.SampleImplementation
{
	public sealed class VelocityComponent : Component
	{
		public override void Delete(int index)
		{
			RunSpeed.Delete(index);
			TurnSpeed.Delete(index);
			CurrentMoveBy.Delete(index);
			CurrentRotationSpeed.Delete(index);
		}

		public Bag<float> RunSpeed { get; } = new Bag<float>();

		public Bag<float> TurnSpeed { get; } = new Bag<float>();

		public Bag<Vector3> CurrentMoveBy { get; } = new Bag<Vector3>();

		public Bag<float> CurrentRotationSpeed { get; } = new Bag<float>();
	}
}
