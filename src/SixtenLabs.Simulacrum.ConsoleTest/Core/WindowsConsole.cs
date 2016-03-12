using System;

namespace SixtenLabs.Simulacrum.ConsoleTest
{
	/// <summary>
	/// Wrapping Windows console commands for DI.
	/// </summary>
	public class WindowsConsole : IConsole
	{
		public WindowsConsole()
		{
		}

		public void SetFullScreen()
		{
			Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
			Console.SetWindowPosition(0, 0);
			Console.CursorVisible = false;
		}

		public void SetWindowSize(int width, int height)
		{
			Console.SetWindowSize(width, height);
		}

		public ConsoleKeyInfo ReadKey(bool intercept = true)
		{
			return Console.ReadKey(intercept);
		}

		public void WriteLine(string message)
		{
			Console.WriteLine(message);
		}

		public bool KeyAvailable
		{
			get
			{
				return Console.KeyAvailable;
			}
		}
	}
}
