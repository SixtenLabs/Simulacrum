using System;

namespace SixtenLabs.Simulacrum
{
  /// <summary>
	/// An Entity Handle is as close as we get to actually having an entity. 
	/// The handle holds the ID of the entity, an lookup index, and an Aspect Mask.
	/// </summary>
	public struct EntityHandle
  {
    public EntityHandle(Guid entity, int index, int aspectSize)
    {
      Aspect = new Aspect(aspectSize);
      Entity = entity;
      Index = index;
    }

    /// <summary>
    /// This index is used for any components that belong to this entity.
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// A unique ID that tags each game-object as a separate item
    /// </summary>
    public Guid Entity { get; }

    /// <summary>
    /// This is used to identify if this entity is a candidate to be processed by a processor.
    /// </summary>
    public Aspect Aspect { get; }
  }
}
