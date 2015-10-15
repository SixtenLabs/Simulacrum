using System;

namespace SixtenLabs.Simulacrum
{
	/// <summary>
	/// A bucket to hold component data in the correct index position.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Bucket<T>
	{
		public Bucket(int initialCapacity = 1000)
		{
			DataBag = new T[initialCapacity];
		}

		private void Grow()
		{
			Grow((int)(DataBag.Length * 1.5) + 1);
		}

		private void Grow(int newCapacity)
		{
			T[] oldElements = DataBag;
			DataBag = new T[newCapacity];
			Array.Copy(oldElements, 0, DataBag, 0, oldElements.Length);
		}

		/// <summary>Removes the specified index.</summary>
		/// <param name="index">The index.</param>
		/// <returns>The removed element.</returns>
		public T Remove(int index)
		{
			// Make copy of element to remove so it can be returned.
			T result = DataBag[index];
			--this.Count;

			// Overwrite item to remove with last element.
			DataBag[index] = DataBag[this.Count];

			// Null last element, so garbage collector can do its work.
			DataBag[this.Count] = default(T);
			return result;
		}

		/// <summary>
		/// <para>Removes the first occurrence of the specified element from this Bag, if it is present.</para>
		/// <para>If the Bag does not contain the element, it is unchanged.</para>
		/// <para>Does this by overwriting it was last element then removing last element.</para>
		/// </summary>
		/// <param name="element">The element to be removed from this list, if present.</param>
		/// <returns><see langword="true"/> if this list contained the specified element, otherwise <see langword="false"/>.</returns>
		public bool Remove(T element)
		{
			for (int index = this.Count - 1; index >= 0; --index)
			{
				if (element.Equals(DataBag[index]))
				{
					--this.Count;

					// Overwrite item to remove with last element.
					DataBag[index] = DataBag[this.Count];
					DataBag[this.Count] = default(T);

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// An array to hold all the data in the correct index position (The EntityHandle holds the index position used for storing all of its components data).
		/// </summary>
		private T[] DataBag { get; set; }

		public int Count { get; private set; }

		public T this[int index]
		{
			get
			{
				return DataBag[index];
			}

			set
			{
				if (index >= DataBag.Length)
				{
					Grow(index * 2);
					Count = index + 1;
				}
				else if (index >= Count)
				{
					Count = index + 1;
				}

				DataBag[index] = value;
			}
		}
	}
}
