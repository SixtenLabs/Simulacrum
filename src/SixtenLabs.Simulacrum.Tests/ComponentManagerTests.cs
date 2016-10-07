using Xunit;
using FluentAssertions;

using SixtenLabs.Simulacrum.ConsoleTest;
using System.Collections.Generic;

namespace SixtenLabs.Simulacrum.Tests
{
  public class ComponentManagerTests
  {
    private ComponentManager NewSubjectUnderTest(int size)
    {
      var components = new List<IComponent>();
      components.Add(new TransformComponent() { AspectMask = 0 });
      components.Add(new VelocityComponent() { AspectMask = 1 });

      return new ComponentManager(components);
    }

    [Fact]
    public void Count_Returns_CorrectNumberOfComponents()
    {
      var subject = NewSubjectUnderTest(4);

      subject.Count.Should().Be(2);
    }

    [Fact]
    public void AspectMask_Returns_CorrectMask()
    {
      var subject = NewSubjectUnderTest(4);

      subject.AspectMask(typeof(TransformComponent)).Should().Be(0);
      subject.AspectMask(typeof(VelocityComponent)).Should().Be(1);
    }

    [Fact]
    public void GetComponent_Returns_CorrectNumberOfComponents()
    {
      var subject = NewSubjectUnderTest(4);

      var actual = subject.GetComponent<TransformComponent>();

      actual.Should().NotBeNull();
    }

    //[Fact]
    //public void DeleteComponentValues_Returns_xx()
    //{
    //	var subject = NewSubjectUnderTest(4);

    //	var transform = subject.GetComponent<TransformComponent>();
    //	transform.Position[1] = Vector3.One;
    //	transform.Orientation[1] = new Quaternion(Vector3.One, 3);

    //	var velocity = subject.GetComponent<VelocityComponent>();
    //	velocity.RunSpeed[1] = 4.0f;
    //	velocity.TurnSpeed[1] = 5.0f;
    //	velocity.CurrentMoveBy[1] = Vector3.One;

    //	subject.DeleteComponentValues(1);

    //	transform.Position[1].Should().Be(Vector3.Zero);
    //	transform.Orientation[1].Should().Be(new Quaternion(0, 0, 0, 0));

    //	velocity.RunSpeed[1].Should().Be(0.0f);
    //	velocity.TurnSpeed[1].Should().Be(0.0f);
    //	velocity.CurrentMoveBy[1].Should().Be(Vector3.Zero);
    //}
  }
}
