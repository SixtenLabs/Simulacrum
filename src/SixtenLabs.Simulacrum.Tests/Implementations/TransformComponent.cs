namespace SixtenLabs.Simulacrum.Tests
{
  public sealed class TransformComponent : Component
  {
    public override void Delete(int index)
    {
      X.Delete(index);
      Y.Delete(index);
    }

    public Bag<int> X { get; } = new Bag<int>();

    public Bag<int> Y { get; } = new Bag<int>();
  }
}
