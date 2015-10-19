using System.Numerics;

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

		public Bag<Vector3> Position { get; } = new Bag<Vector3>();

		public Bag<Quaternion> Orientation { get; } = new Bag<Quaternion>();

		public Bag<Vector3> Scale { get; } = new Bag<Vector3>();
	}
}
