using System;

namespace SixtenLabs.Simulacrum.ConsoleTest
{
	public class RenderProcessor : EntityProcessor
	{
		public RenderProcessor(IComponentManagerFactory componentManagerFactory)
      : base(componentManagerFactory)
    {
		}

		protected override void SetupSystemProperties()
		{
			Order = 80;
			EntityProcessorType = EntityProcessorType.Render;
			Name = "Render System";
		}

		protected override void SetupComponentProperties()
		{
			RenderComponent = ComponentManager.GetComponent<RenderComponent>();
			TransformComponent = ComponentManager.GetComponent<TransformComponent>();
		}

		protected override void RegisterRequiredComponents()
		{
			var componentsTypes = new[]
			{
				typeof(TransformComponent),
				typeof(RenderComponent)
			};

			RequiredComponentTypes.AddRange(componentsTypes);
		}

		protected override void RegisterOptionalComponents()
		{
		}

		public override void Process(ISimulator simulator, double tick)
		{
			foreach (var handle in simulator.GetHandlesForProcessor(Aspect))
			{
				Console.BackgroundColor = RenderComponent.Color[handle.Index];
				Console.Clear();

				var x = TransformComponent.X[handle.Index];
				var y = TransformComponent.Y[handle.Index];
				Console.SetCursorPosition(x, y);

				Console.Write(RenderComponent.Text[handle.Index]);
			}
		}

		public override void Dispose()
		{
		}

		private RenderComponent RenderComponent { get; set; }

		private TransformComponent TransformComponent { get; set; }
	}
}
