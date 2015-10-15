namespace SixtenLabs.Simulacrum
{
	/// <summary>
	/// A component represents all the data for one aspect of an object.
	/// No code is allowed in a component
	///  
	/// example:
	/// 
	/// TransformComponent
	///   Position
	///   Rotation
	///   Scale
	/// 
	/// Further this component type holds the data in property buckets
	/// indexed by the data's EntityHandle.
	/// 
	/// </summary>
	public interface IComponent
	{
		/// <summary>
		/// This is set by Simulation.cs when the component is registered.
		/// </summary>
		int ComponentTypeMask { get; set; }

		void Delete(int index);
	}
}
