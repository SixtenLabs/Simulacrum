namespace SixtenLabs.Simulacrum
{
	/// <summary>
	/// All the data for one aspect of an object.
	/// </summary>
	public abstract class Component : IComponent
	{
		public int AspectMask { get; set; }

		public abstract void Delete(int index);
	}
}
