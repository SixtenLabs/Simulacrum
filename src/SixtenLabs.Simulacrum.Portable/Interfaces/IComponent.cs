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
    /// This is the index of the AspectMask for this component. This is used to test signatures for Processors
    /// to know if this component is used by a processor.
    /// This is set by the ComponentManager when the component is registered.
    /// </summary>
    int AspectMask { get; set; }

    void Delete(int index);
  }
}
