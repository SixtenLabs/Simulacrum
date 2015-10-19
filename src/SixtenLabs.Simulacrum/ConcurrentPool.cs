using System;
using System.Collections.Concurrent;

namespace SixtenLabs.Simulacrum
{
	public class ConcurrentPool<T> where T : class
	{
		public ConcurrentPool(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}

			if (typeof(T).GetConstructor(Type.EmptyTypes) == null)
			{
				throw new ArgumentException($"Type '{typeof(T).Name}' doesn't have a parameterless constructor. Specify a factoryMethod.");
			}

			FactoryMethod = () => Activator.CreateInstance<T>();
			Initialize(capacity);
		}

		public ConcurrentPool(int capacity, Func<T> factoryMethod)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}

			if (factoryMethod == null)
			{
				throw new ArgumentNullException("factoryMethod");
			}

			FactoryMethod = factoryMethod;
			Initialize(capacity);
		}

		private void Initialize(int capacity)
		{
			for (int i = 0; i < capacity; i++)
			{
				Bag.Add(FactoryMethod());
			}
		}

		public virtual T Take()
		{
			T item;

			return Bag.TryTake(out item) ? item : FactoryMethod();
		}

		public virtual void Add(T item)
		{
			Bag.Add(item);
		}

		private ConcurrentBag<T> Bag { get; } = new ConcurrentBag<T>();

		private Func<T> FactoryMethod { get; set; }
	}
}
