using System;

namespace SixtenLabs.Simulacrum.ConsoleTest
{
	public class RenderProcessor : EntityProcessor
	{
		public RenderProcessor()
    {
		}

		protected override void SetupSystemProperties()
		{
			Order = 80;
			EntityProcessorType = EntityProcessorType.Render;
			Name = "Render System";
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
      var transformComponent = simulator.GetComponent<TransformComponent>();
      var renderComponent = simulator.GetComponent<RenderComponent>();

      foreach (var handle in simulator.GetHandlesForProcessor(Aspect))
			{
				Console.BackgroundColor = renderComponent.Color[handle.Index];
				Console.Clear();

				var x = transformComponent.X[handle.Index];
				var y = transformComponent.Y[handle.Index];
				Console.SetCursorPosition(x, y);

				Console.Write(renderComponent.Text[handle.Index]);
			}
		}

		public override void Dispose()
		{
		}
	}
}
