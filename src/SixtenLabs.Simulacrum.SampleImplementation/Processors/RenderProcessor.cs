using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Simulacrum.SampleImplementation
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
