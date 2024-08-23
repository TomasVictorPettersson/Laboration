using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.ConsoleUI.Utils;
using Laboration.GameFactory.Creators;
using Laboration.GameFactory.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;

namespace Laboration.GameApplication
{
	public static class Program
	{
		// Game factory instance
		public static IGameFactory? Factory { get; set; }

		// Game selector instance
		public static IGameSelector? GameSelector { get; set; }

		// Entry point of the application. Initializes the game selector, runs the game loop,
		// and waits for the user to close the window.

		public static void Main()
		{
			try
			{
				InitializeGameSelector();
				RunGameLoop();
				ConsoleUtils.WaitForUserToContinue(PromptMessages.CloseWindowPrompt);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An unexpected error occurred: {ex.Message}");
			}
		}

		// Creates and initializes a new instance of the game selector.

		public static void InitializeGameSelector()
		{
			GameSelector = new GameSelector();
		}

		// Runs the main game loop. Continuously prompts the user to select a game type,
		// initializes game factory and dependencies, and executes the game loop until
		// the user chooses to quit.

		public static void RunGameLoop()
		{
			GameTypes selectedGameType;

			do
			{
				selectedGameType = GameSelector!.SelectGameType();

				if (selectedGameType == GameTypes.Quit)
					break;

				if (!InitializeGameFactory(selectedGameType))
				{
					Console.WriteLine("Failed to create game factory. Please try again.");
					continue;
				}

				var (userInterface, gameLogic) = InitializeDependencies();
				var gameFlowController = Factory!.CreateGameFlowController();
				gameFlowController.ExecuteGameLoop(userInterface, gameLogic);
			} while (selectedGameType != GameTypes.Quit);
		}

		// Initializes the game factory based on the selected game type.

		public static bool InitializeGameFactory(GameTypes gameType)
		{
			Factory = FactoryCreator.CreateFactory(gameType);
			return Factory != null;
		}

		// Initializes and returns the user interface and game logic dependencies.

		public static (IConsoleUI, IGameLogic) InitializeDependencies()
		{
			var dependencyInitializer = Factory!.CreateDependencyInitializer();
			return dependencyInitializer.InitializeDependencies();
		}
	}
}