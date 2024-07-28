using Laboration.DependencyInjection.Classes;
using Laboration.GameFlow.Classes;
using Laboration.GameFlow.Interfaces;

namespace Laboration.Application
{
	// The main entry point of the application. Initializes dependencies,
	// creates instances of necessary classes, and executes the game loop.
	public static class Program
	{
		// The entry point of the application.

		public static void Main()
		{
			// Initialize dependencies using DependencyInitializer
			var dependencyInitializer = new DependencyInitializer();
			var (userInterface, gameLogic) = dependencyInitializer.InitializeDependencies();

			// Create an instance of GameFlowController
			IGameFlowController gameFlowController = new GameFlowController();

			try
			{
				// Execute the game loop using the user interface and game logic
				gameFlowController.ExecuteGameLoop(userInterface, gameLogic);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}
	}
}