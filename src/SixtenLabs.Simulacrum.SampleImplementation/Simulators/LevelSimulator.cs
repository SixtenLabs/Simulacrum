namespace SixtenLabs.Simulacrum.SampleImplementation.Simulators
{
	public class LevelSimulator : Simulator
	{
		public LevelSimulator(IComponentManager componentManager)
			: base(componentManager)
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
