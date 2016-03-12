using System;

namespace SixtenLabs.Simulacrum
{
	public interface ISimulation : IDisposable
	{
		ISimulator GetSimulatorByName(string name);

		void ActivateSimulator(string name);

		void DeactivateSimulator(string name);

		void Load();

		void Update(double tick);

		void Render(double tick);
	}
}
