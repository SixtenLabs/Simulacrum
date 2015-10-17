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

		public Bucket<float> RunSpeed { get; } = new Bucket<float>();

		public Bucket<float> TurnSpeed { get; } = new Bucket<float>();

		public Bucket<Vector3> CurrentMoveBy { get; } = new Bucket<Vector3>();

		public Bucket<float> CurrentRotationSpeed { get; } = new Bucket<float>();
	}
}
