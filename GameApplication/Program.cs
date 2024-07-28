using Laboration.DependencyInjection.Implementations;
using Laboration.GameFlow.Implementations;

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
			var bullsAndCowsDependencyInitializer = new BullsAndCowsDependencyInitializer();
			var (userInterface, gameLogic) = bullsAndCowsDependencyInitializer.InitializeDependencies();

			// Create an instance of GameFlowController
			var gameFlowController = new BullsAndCowsGameFlowController();

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