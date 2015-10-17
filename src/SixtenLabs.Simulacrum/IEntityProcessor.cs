using System;
using System.Collections.Generic;

namespace SixtenLabs.Simulacrum
{
	public interface IEntityProcessor : IDisposable
	{
		/// <summary>
		/// These component types are required by this system and 
		/// compose the ComponentTypeMask
		/// </summary>
		List<Type> RequiredComponentTypes { get; }

		/// <summary>
		/// These are components that this system might use but are not required
		/// and are not part of the ComponentTypeMask
		/// </summary>
		List<Type> OptionalComponentTypes { get; }

		/// <summary>
		/// 
		/// </summary>
		Aspect Aspect { get; set; }

		int Order { get; }

		void Process(ISimulator space, double tick);

		void Load();

		EntityProcessorType EntitySystemType { get; }
	}
}
