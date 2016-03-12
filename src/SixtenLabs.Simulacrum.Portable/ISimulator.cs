using System;
using System.Collections.Generic;

namespace SixtenLabs.Simulacrum
{
	/// <summary>
	/// A logical way to seperate groups of entities.
	/// 
	/// For example:
	/// 
	/// Simulator 1 : game menu
	/// Simulator 2 : current scene or level
	/// Simulator 3 : Cutscene
	/// Simulator 4 : Next scene or level
	/// </summary>
	public interface ISimulator : IDisposable
	{
		/// <summary>
		/// Get all components for a specific Component Type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T GetComponent<T>() where T : class, IComponent;

		EntityHandle CreateEntity();

		void DeleteEntity(EntityHandle handle);

		void Load();

		IList<EntityHandle> GetHandlesForProcessor(Aspect processorComponentMask);


		int Order { get; set; }

		string Name { get; set; }

	}
}
