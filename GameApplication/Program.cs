using Laboration.ConsoleUI.GameSelection;
using Laboration.GameFactory.Creators;
using Laboration.GameFactory.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameApplication
{
	// The main entry point of the application.
	public static class Program
	{
		// Factory for creating game-related components.
		// This allows easy substitution of different implementations, useful for testing.
		public static IGameFactory? Factory { get; set; }

		// Entry point of the application.
		public static void Main()
		{
			GameTypes selectedGameType;

			// Game loop to continuously prompt the user to select a game and play it.
			do
			{
				selectedGameType = GameSelector.SelectGameType();

				// Exit the game loop if the user selects to quit.
				if (selectedGameType == GameTypes.Quit)
					break;

				// Initialize the appropriate factory based on the user's choice.
				Factory = FactoryCreator.CreateFactory(selectedGameType);

				// Handle the case where the factory initialization fails.
				if (Factory == null)
				{
					Console.WriteLine("Game initialization failed. Please try again.");
					continue;
				}

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
			while (selectedGameType != GameTypes.Quit);

			// Display a farewell message when the user quits the game.
			Console.WriteLine("Thank you for playing!");
		}
	}
}