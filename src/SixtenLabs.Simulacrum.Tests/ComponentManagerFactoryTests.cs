using Xunit;
using FluentAssertions;
using NSubstitute;

using System;
using System.Collections.Generic;

namespace SixtenLabs.Simulacrum.Tests
{
  public class ComponentManagerFactoryTests
  {
    public ComponentManagerFactoryTests()
    {
      MockComponentFactory = Substitute.For<IComponentFactory>();
    }

    private ComponentManagerFactory NewSubjectUnderTest()
    {
      var factory = new ComponentManagerFactory(MockComponentFactory);

      return factory;
    }

    [Fact]
    public void CreateComponentManager()
    {
      var subject = NewSubjectUnderTest();

      var actual = subject.CreateComponentManager();

      actual.Should().NotBeNull();
    }

    [Fact]
    public void CreateComponentManager_CalledTwice_ThrowsException()
    {
      var subject = NewSubjectUnderTest();

      subject.CreateComponentManager();

      Action act = () => subject.CreateComponentManager();

      act.ShouldThrow<InvalidOperationException>().WithMessage("The components have already been registered for this instance of the simulation. This method can only be called once.");
    }

    [Fact]
    public void CreateComponentManager_RegistersAllComponents_WithComponentManager()
    {
      var subject = NewSubjectUnderTest();

      var components = AllComponents();

      MockComponentFactory.GetAllComponents().Returns(components);

      var actual = subject.CreateComponentManager();

      actual.Count.Should().Be(4);
    }

    [Fact]
    public void CreateComponentManager_RegistersRequestedComponents_WithComponentManager()
    {
      var subject = NewSubjectUnderTest();

      var components = AllComponents();
      MockComponentFactory.GetAllComponents().Returns(components);

      subject.CreateComponentManager();

      MockComponentFactory.GetComponentByType(typeof(TransformComponent)).Returns(new TransformComponent());
      MockComponentFactory.GetComponentByType(typeof(VelocityComponent)).Returns(new VelocityComponent());
      MockComponentFactory.GetComponentByType(typeof(RenderComponent)).Returns(new RenderComponent());

      var actual = subject.CreateComponentManager(new[] { typeof(TransformComponent), typeof(VelocityComponent), typeof(RenderComponent) });

      actual.Count.Should().Be(3);
    }

    [Fact]
    public void CreateComponentManager_DifferentComponentManagers_ComponentInstancesAreNotTheSame()
    {
      var subject = NewSubjectUnderTest();

      var components = AllComponents();
      MockComponentFactory.GetAllComponents().Returns(components);

      var simulationComponentManager = subject.CreateComponentManager();

      MockComponentFactory.GetComponentByType(typeof(TransformComponent)).Returns(new TransformComponent());
      MockComponentFactory.GetComponentByType(typeof(VelocityComponent)).Returns(new VelocityComponent());
      MockComponentFactory.GetComponentByType(typeof(RenderComponent)).Returns(new RenderComponent());

      var actual = subject.CreateComponentManager(new[] { typeof(TransformComponent), typeof(VelocityComponent), typeof(RenderComponent) });

      var transform1 = simulationComponentManager.GetComponent<TransformComponent>();
      var transform2 = actual.GetComponent<TransformComponent>();

      transform2.AspectMask.Should().Be(transform1.AspectMask);
      transform2.GetHashCode().Should().NotBe(transform1.GetHashCode());
    }

    [Fact]
    public void CreateComponentManager_DifferentComponentManagers_AspectMasksAreSameAcrossInstances()
    {
      var subject = NewSubjectUnderTest();

      var components = AllComponents();
      MockComponentFactory.GetAllComponents().Returns(components);

      var simulationComponentManager = subject.CreateComponentManager();

      MockComponentFactory.GetComponentByType(typeof(TransformComponent)).Returns(new TransformComponent());
      MockComponentFactory.GetComponentByType(typeof(VelocityComponent)).Returns(new VelocityComponent());
      MockComponentFactory.GetComponentByType(typeof(RenderComponent)).Returns(new RenderComponent());

      var actual = subject.CreateComponentManager(new[] { typeof(TransformComponent), typeof(VelocityComponent), typeof(RenderComponent) });

      var transform1 = simulationComponentManager.GetComponent<TransformComponent>();
      var transform2 = actual.GetComponent<TransformComponent>();

      transform2.AspectMask.Should().Be(transform1.AspectMask);
    }

    private IList<IComponent> AllComponents()
    {
      var components = new List<IComponent>();

      components.Add(new TransformComponent());
      components.Add(new VelocityComponent());
      components.Add(new RenderComponent());
      components.Add(new UiComponent());

      return components;
    }

    private IComponentFactory MockComponentFactory { get; set; }
  }
}
