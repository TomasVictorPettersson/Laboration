using Laboration.DependencyInjection.Implementations;
using Laboration.GameFlow.Implementations;

namespace Laboration.Application
{
	// Main entry point of the Bulls and Cows application.
	public static class Program
	{
		// Entry point of the application.
		public static void Main()
		{
			// Initialize dependencies
			var dependencyInitializer = new BullsAndCowsDependencyInitializer();
			var (userInterface, gameLogic) = dependencyInitializer.InitializeDependencies();

			try
			{
				// Create and configure the GameFlowController with dependencies
				var gameFlowController = new BullsAndCowsGameFlowController();

				// Execute the game loop with the provided user interface and game logic
				gameFlowController.ExecuteGameLoop(userInterface, gameLogic);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}
	}
}