using System;

namespace SixtenLabs.Simulacrum
{
	public class Bag<T>
	{
		public Bag(int initialSize = 16)
		{
			Elements = new T[initialSize];
		}

		private void Grow()
		{
			Grow((int)(Elements.Length * 1.5) + 1);
		}

		/// <summary>Grows the specified new capacity.</summary>
		/// <param name="newCapacity">The new capacity.</param>
		private void Grow(int newCapacity)
		{
			T[] oldElements = Elements;
			Elements = new T[newCapacity];
			Array.Copy(oldElements, 0, Elements, 0, oldElements.Length);
		}

		/// <summary>Returns the element at the specified position in Bag.</summary>
		/// <param name="index">The index.</param>
		/// <returns>The element from the specified position in Bag.</returns>
		public T this[int index]
		{
			get
			{
				return Elements[index];
			}

			set
			{
				if (index >= Elements.Length)
				{
					Grow(index * 2);
					Count = index + 1;
				}
				else if (index >= this.Count)
				{
					Count = index + 1;
				}

				Elements[index] = value;
			}
		}

		private T[] Elements { get; set; }

		public int Count { get; private set; }

		public int Capacity
		{
			get
			{
				return Elements.Length;
			}
		}
	}
}
