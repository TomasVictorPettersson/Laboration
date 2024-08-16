using Laboration.GameFactory.Implementations;
using Laboration.GameFactory.Interfaces;
using Laboration.GameResources.Constants;
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
				selectedGameType = SelectGameType();

				// Exit the game loop if the user selects to quit.
				if (selectedGameType == GameTypes.Quit)
					break;

				// Initialize the appropriate factory based on the user's choice.
				Factory = CreateFactory(selectedGameType);

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

		// Method to prompt the user to select a game type.
		private static GameTypes SelectGameType()
		{
			Console.WriteLine("Select a game to play:");
			Console.WriteLine($"1. {nameof(GameTypes.BullsAndCows)}");
			Console.WriteLine($"2. {nameof(GameTypes.MasterMind)}");
			Console.WriteLine($"3. {nameof(GameTypes.Quit)}");

			// Loop until the user enters a valid selection.
			while (true)
			{
				Console.Write(UserInteractionMessages.ChoseGamePrompt);
				var input = Console.ReadLine();

				switch (input)
				{
					case "1":
						return GameTypes.BullsAndCows;

					case "2":
						return GameTypes.MasterMind;

					case "3":
						return GameTypes.Quit;

					default:
						Console.WriteLine("Invalid selection. Please enter 1, 2, or 3.");
						break;
				}
			}
		}

		// Method to create the appropriate factory based on the selected game type.
		private static IGameFactory? CreateFactory(GameTypes gameType)
		{
			return gameType switch
			{
				GameTypes.BullsAndCows => new BullsAndCowsGameFactory(),
				GameTypes.MasterMind => new MasterMindGameFactory(),
				GameTypes.Quit => null, // Explicitly handle the Quit game type by returning null
				_ => throw new ArgumentException("Invalid game type", nameof(gameType)),
			};
		}
	}
}