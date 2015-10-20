namespace SixtenLabs.Simulacrum.SampleImplementation.Processors
{
	public class MovementProcessor : EntityProcessor
	{
		public MovementProcessor(IComponentManagerFactory componentManagerFactory)
      : base(componentManagerFactory)
    {
		}

		protected override void SetupSystemProperties()
		{
			Order = 20;
			EntitySystemType = EntityProcessorType.Update;
			Name = "Movement System";
		}

		protected override void SetupComponentProperties()
		{
			TransformComponent = ComponentManager.GetComponent<TransformComponent>();
			VelocityComponent = ComponentManager.GetComponent<VelocityComponent>();
		}

		protected override void RegisterRequiredComponents()
		{
			var componentsTypes = new[]
			{
				typeof(TransformComponent),
				typeof(VelocityComponent)
			};

			RequiredComponentTypes.AddRange(componentsTypes);
		}

		protected override void RegisterOptionalComponents()
		{
		}

		public override void Process(ISimulator simulator, double tick)
		{
			// Do something interesting here.
		}

		public override void Dispose()
		{
		}

		private TransformComponent TransformComponent { get; set; }

		private VelocityComponent VelocityComponent { get; set; }
	}
}
