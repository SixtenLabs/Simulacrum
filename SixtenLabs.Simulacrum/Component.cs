namespace SixtenLabs.Simulacrum
{
	/// <summary>
	/// All the data for one aspect of an object.
	/// 
	/// This object
	/// </summary>
	public abstract class Component : IComponent
	{
		public int ComponentTypeMask { get; set; }

		public abstract void Delete(int index);
	}
}
