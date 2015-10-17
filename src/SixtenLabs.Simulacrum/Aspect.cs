using System.Collections;

namespace SixtenLabs.Simulacrum
{
	/// <summary>
	/// A signature of Component types that a Processor requires to process an Entity.
	/// </summary>
	public class Aspect
	{
		public Aspect(int size)
		{
			Masks = new BitArray(size);
		}

		/// <summary>
		/// Determines whether the given Mask is set.
		/// </summary>
		/// <param name="index">The index of the Mask to check.</param>
		/// <returns><c>true</c> if the Mask is set; otherwise, <c>false</c>.</returns>
		public bool HasMask(int index)
		{
			return Masks[index];
		}

		/// <summary>
		/// Add a Mask at the given index.
		/// </summary>
		/// <param name="index">the index of the mask</param>
		public void AddMask(int index)
		{
			Masks.Set(index, true);
		}

		/// <summary>
		/// Remove a Mask at the given index.
		/// </summary>
		/// <param name="index">the index of the mask</param>
		public void RemoveMask(int index)
		{
			Masks.Set(index, false);
		}

		/// <summary>
		/// Determines whether all of the bits in this instance are also set in the given Aspect.
		/// </summary>
		/// <param name="other">The Aspect to check.</param>
		/// <returns>
		/// <c>true</c> if all of the bits in this instance are set in <paramref name="aspectToTest"/>; otherwise, <c>false</c>.
		/// </returns>
		public bool IsSubsetOf(Aspect aspectToTest)
		{
			var tempSet = new Aspect(Masks.Length);

			for (int i = 0; i < Masks.Count; i++)
			{
				if (Masks[i] && (!(aspectToTest.Masks[i])))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Collection To Hold the aspect masks
		/// </summary>
		private BitArray Masks { get; }
	}
}
