using System;

namespace SixtenLabs.Simulacrum.Tests
{
  public class RenderComponent : Component
  {
    public override void Delete(int index)
    {
      Text.Delete(index);
      Color.Delete(index);
    }

    public Bag<string> Text { get; } = new Bag<string>();

    public Bag<ConsoleColor> Color { get; } = new Bag<ConsoleColor>();
  }
}
