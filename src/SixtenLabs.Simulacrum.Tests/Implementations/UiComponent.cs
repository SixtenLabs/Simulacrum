namespace SixtenLabs.Simulacrum.Tests
{
  public class UiComponent : Component
  {
    public override void Delete(int index)
    {
      Name.Delete(index);
    }

    public Bag<string> Name { get; } = new Bag<string>();
  }
}
