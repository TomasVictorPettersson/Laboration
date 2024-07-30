using Laboration.GameFactory.Implementations;
using Laboration.GameFactory.Interfaces;

namespace Laboration.GameApplication
{
	// The main entry point of the application.
	// Initializes dependencies, creates necessary instances, and runs the game loop.
	public static class Program
	{
		// Factory for creating game-related components.
		// This allows easy substitution of different implementations, useful for testing.
		public static IGameFactory Factory { get; set; }
			= new BullsAndCowsGameFactory();

		// Entry point of the application.
		public static void Main()
		{
			// Initialize dependencies using the factory.
			var dependencyInitializer = Factory.CreateDependencyInitializer();
			var (userInterface, gameLogic) = dependencyInitializer.InitializeDependencies();

			try
			{
				// Create and configure the GameFlowController using the factory.
				var gameFlowController = Factory.CreateGameFlowController();
				// Execute the game loop with the provided user interface and game logic.
				gameFlowController.ExecuteGameLoop(userInterface, gameLogic);
			}
			catch (Exception ex)
			{
				// Handle and display any exceptions that occur during the game loop execution.
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}
	}
}