using Laboration.GameFactory.Implementations;
using Laboration.GameFactory.Interfaces;

namespace Laboration.GameApplication
{
	public static class Program
	{
		// Public property to get the factory methods for IDependencyInitializer and IGameFlowController.
		// This allows for easy substitution of different implementations, especially for testing.
		public static IGameFactory Factory { get; set; }
			= new BullsAndCowsGameFactory();

		// Entry point of the application.
		public static void Main()
		{
			// Initialize dependencies using the factory method.
			var dependencyInitializer = Factory.CreateDependencyInitializer();
			var (userInterface, gameLogic) = dependencyInitializer.InitializeDependencies();

			try
			{
				// Create and configure the GameFlowController using the factory method.
				var gameFlowController = Factory.CreateGameFlowController();
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