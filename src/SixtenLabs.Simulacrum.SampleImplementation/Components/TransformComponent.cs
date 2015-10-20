using System.Numerics;

namespace SixtenLabs.Simulacrum.SampleImplementation
{
	public sealed class TransformComponent : Component
	{
		public override void Delete(int index)
		{
			Position.Delete(index);
			Orientation.Delete(index);
			Scale.Delete(index);
		}

		public Bag<Vector3> Position { get; } = new Bag<Vector3>();

		public Bag<Quaternion> Orientation { get; } = new Bag<Quaternion>();

		public Bag<Vector3> Scale { get; } = new Bag<Vector3>();
	}
}
