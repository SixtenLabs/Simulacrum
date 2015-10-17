using System;
using System.Collections.Concurrent;

namespace SixtenLabs.Simulacrum
{
	public class EntityHandle
	{
		private static int nextIndex;

		public EntityHandle(Guid entity, int aspectSize)
		{
			Aspect = new Aspect(aspectSize);
			Entity = entity;
			GetIndex();
		}

		private void GetIndex()
		{
			int index;
			var result = UsedIndexPool.TryDequeue(out index);

			if (!result)
			{
				Index = nextIndex;
				nextIndex++;
			}
		}

		public void AddComponentTypeMask(int componentTypeMask)
		{
			Aspect.AddMask(componentTypeMask);
		}

		public void RemoveComponentTypeMask(int componentTypeMask)
		{
			Aspect.RemoveMask(componentTypeMask);
		}

		/// <summary>
		/// This index is used for any components that belong to this entity.
		/// </summary>
		public int Index { get; set; }

		/// <summary>
		/// A unique ID that tags each game-object as a separate item
		/// </summary>
		public Guid Entity { get; }

		/// <summary>
		/// 
		/// </summary>
		public Aspect Aspect { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public static ConcurrentQueue<int> UsedIndexPool { get; } = new ConcurrentQueue<int>();
	}
}
