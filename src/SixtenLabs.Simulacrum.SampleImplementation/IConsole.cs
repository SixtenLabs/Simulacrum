using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Simulacrum.SampleImplementation
{
	public interface IConsole
	{
		void SetFullScreen();

		void SetWindowSize(int width, int height);

		ConsoleKeyInfo ReadKey(bool intercept = true);

		void WriteLine(string message);

		bool KeyAvailable { get; }
	}
}
