using System;
using System.Collections;

namespace SixtenLabs.Simulacrum
{
	/// <summary>
	/// A signature of Component types that a Processor requires to process an Entity.
	/// A resizable collection of bits.
	/// </summary>
	public class Aspect
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Aspect"/> class.
		/// </summary>
		public Aspect(int size = 6)
		{
			Bits = new BitArray(size);
		}

		/// <summary>
		/// Sets the bit at the given index.
		/// </summary>
		/// <param name="index">The bit to set.</param>
		private void Set(int index, bool value)
		{
			if (Bits.Length <= index)
			{
				Bits.Length = Bits.Length + 1;
			}

			Bits.Set(index, value);
		}

		/// <summary>
		/// Determines whether the given bit is set.
		/// </summary>
		/// <param name="index">The index of the bit to check.</param>
		/// <returns><c>true</c> if the bit is set; otherwise, <c>false</c>.</returns>
		public bool IsSet(int index)
		{
			if(Bits.Length <= index)
			{
				return false;
			}

			return Bits[index];
		}

		public bool GetBit(int index)
		{
			return Bits.Get(index);
		}

		public void Add(int index)
		{
			Set(index, true);
		}

		public void Remove(int index)
		{
			Set(index, false);
		}

		/// <summary>
		/// Sets all bits.
		/// </summary>
		public void SetAll()
		{
			Bits.SetAll(true);
		}

		/// <summary>
		/// Clears all bits.
		/// </summary>
		public void ClearAll()
		{
			Bits.SetAll(false);
		}

		/// <summary>
		/// Determines whether all of the bits in this instance are also set in the given bitset.
		/// </summary>
		/// <param name="other">The bitset to check.</param>
		/// <returns><c>true</c> if all of the bits in this instance are set in <paramref name="other"/>; otherwise, <c>false</c>.</returns>
		public bool IsSubsetOf(Aspect other)
		{
			var tempSet = new Aspect();

			for (int i = 0; i < Bits.Count; i++)
			{
				if (Bits.Get(i) && (!(other.GetBit(i))))
				{
					return false;
				}
			}

			return true;
		}

		public Aspect Union(Aspect aSet)
		{
			var tempSet = new Aspect();

			for (int i = 0; i < Bits.Count; i++)
			{
				var value = (Bits[i] || aSet.GetBit(i));
				tempSet.Set(i, value); 
			}

			return tempSet;
		}

		public Aspect Intersection(Aspect aSet)
		{
			var tempSet = new Aspect();

			for (int i = 0; i < Bits.Count; i++)
			{
				var value = (Bits[i] && aSet.GetBit(i));
				tempSet.Set(i, value);
			}

			return tempSet;
		}

		public Aspect Difference(Aspect aSet)
		{
			var tempSet = new Aspect();

			for (int i = 0; i < Bits.Count; i++)
			{
				var value = (Bits[i] && (!(aSet.GetBit(i))));
				tempSet.Set(i, value); 
			}

			return tempSet;
		}

		private BitArray Bits { get; }
	}
}
