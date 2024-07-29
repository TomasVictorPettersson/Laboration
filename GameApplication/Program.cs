using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;

namespace Laboration.GameApplication
{
	// Main entry point of the application.
	public static class Program
	{
		// Public properties to get the factory function for IDependencyInitializer and IGameFlowController.
		// This allows for easy substitution of different implementations, especially for testing.
		public static Func<IDependencyInitializer> DependencyInitializerFactory { get; set; }
			= () => new BullsAndCowsDependencyInitializer();

		public static Func<IGameFlowController> GameFlowControllerFactory { get; set; }
			= () => new BullsAndCowsGameFlowController();

		// Entry point of the application.
		public static void Main()
		{
			// Initialize dependencies using the factory method.
			var dependencyInitializer = DependencyInitializerFactory();
			var (userInterface, gameLogic) = dependencyInitializer.InitializeDependencies();

			try
			{
				// Create and configure the GameFlowController using the factory method.
				var gameFlowController = GameFlowControllerFactory();
				// Execute the game loop with the provided user interface and game logic.
				gameFlowController.ExecuteGameLoop(userInterface, gameLogic);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}
	}
}