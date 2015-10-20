namespace SixtenLabs.Simulacrum.SampleImplementation.Simulators
{
	public class LevelSimulator : Simulator
	{
		public LevelSimulator(IComponentManagerFactory componentManagerFactory)
			: base(componentManagerFactory)
		{

		}

		protected override void SetupProperties()
		{
			Order = 1;
			Name = "Planet";
		}

		public override void Load()
		{
		}
	}
}
