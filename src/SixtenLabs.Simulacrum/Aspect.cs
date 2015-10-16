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
		const int BitSize = (sizeof(uint) * 8) - 1;
		const int ByteSize = 5;  // log_2(BitSize + 1)

		private BitArray Bits { get; } = new BitArray(1);

		/// <summary>
		/// Initializes a new instance of the <see cref="Aspect"/> class.
		/// </summary>
		public Aspect(int size = 6)
		{
			Bits = new BitArray(size);
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

		/// <summary>
		/// Sets the bit at the given index.
		/// </summary>
		/// <param name="index">The bit to set.</param>
		public void SetBit(int index, bool value = true)
		{
			if (Bits.Length <= index)
			{
				Bits.Length = Bits.Length + 1;
			}

			Bits.Set(index, value);
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
				tempSet.SetBit(i, value); 
			}

			return tempSet;
		}

		public Aspect Intersection(Aspect aSet)
		{
			var tempSet = new Aspect();

			for (int i = 0; i < Bits.Count; i++)
			{
				var value = (Bits[i] && aSet.GetBit(i));
				tempSet.SetBit(i, value);
			}

			return tempSet;
		}

		public Aspect Difference(Aspect aSet)
		{
			var tempSet = new Aspect();

			for (int i = 0; i < Bits.Count; i++)
			{
				var value = (Bits[i] && (!(aSet.GetBit(i))));
				tempSet.SetBit(i, value); 
			}

			return tempSet;
		}
	}
}
