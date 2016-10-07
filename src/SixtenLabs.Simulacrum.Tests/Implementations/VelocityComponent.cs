namespace SixtenLabs.Simulacrum.Tests
{
  public sealed class VelocityComponent : Component
  {
    public override void Delete(int index)
    {
      MoveByX.Delete(index);
      MoveByY.Delete(index);
    }

    public Bag<int> MoveByX { get; } = new Bag<int>();

    public Bag<int> MoveByY { get; } = new Bag<int>();
  }
}
