using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Simulacrum.Tests.Components
{
	public class TransformComponent : Component
	{
		public override void Delete(int index)
		{
			Position.Remove(index);
			Orientation.Remove(index);
			Scale.Remove(index);
		}

		/// <summary>
		/// 
		/// </summary>
		public Bucket<Vector3> Position { get; } = new Bucket<Vector3>();

		/// <summary>
		/// 
		/// </summary>
		public Bucket<Quaternion> Orientation { get; } = new Bucket<Quaternion>();

		/// <summary>
		/// 
		/// </summary>
		public Bucket<Vector3> Scale { get; } = new Bucket<Vector3>();
	}
}
