using System;
using System.Diagnostics;

namespace SixtenLabs.Simulacrum.ConsoleTest
{
	public class GameWindow
	{
		public GameWindow(ISimulation simulation, IConsole console)
		{
			Simulation = simulation;
			Console = console;
		}

		private void HandleInput()
		{
			ConsoleKeyInfo key = Console.ReadKey();

			switch (key.Key)
			{
				case ConsoleKey.F1:
					Console.WriteLine("You pressed F1!");
					break;
				case ConsoleKey.F2:
					Console.WriteLine("Quitting!");
					IsRunning = false;
					break;
				default:
					break;
			}
		}

		private void Loop()
		{
			var timer = new Stopwatch();
			timer.Start();

			while (IsRunning)
			{
				if(Console.KeyAvailable)
				{
					HandleInput();
				}

				Update(timer.ElapsedMilliseconds);
				Render(timer.ElapsedMilliseconds);
			}

			timer.Stop();
		}

		public void Run()
		{
			Console.WriteLine("Welcome to the Simulacrum Test Console.");
			Console.WriteLine("Please enter some text...");

			Simulation.Load();
			Simulation.ActivateSimulator("Level 1");

			Loop();
		}

		private void Update(long tick)
		{
			Simulation.Update(tick);
		}

		private void Render(long tick)
		{
			Simulation.Render(tick);
		}

		private ISimulation Simulation { get; }

		private bool IsRunning { get; set; } = true;

		private IConsole Console { get; }
	}
}
