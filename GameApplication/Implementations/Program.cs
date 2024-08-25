using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.ConsoleUI.Utils;
using Laboration.GameApplication.Interfaces;
using Laboration.GameFactory.Implementations;
using Laboration.GameFactory.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;

namespace Laboration.GameApplication.Implementations
{
	// The Program class is responsible for initializing and running the game application.
	public class Program(IGameSelector gameSelector, IGameFactoryCreator factoryCreator) : IProgram
	{
		private IGameFactory? _factory;
		private readonly IGameSelector _gameSelector = gameSelector;
		private readonly IGameFactoryCreator _factoryCreator = factoryCreator;

		// Entry point of the application. Initializes Program,
		// runs the game loop, and waits for user to close the window.
		public static void Main()
		{
			try
			{
				var program = new Program(new GameSelector(), new GameFactoryCreator());
				program.RunGameLoop();
				ConsoleUtils.WaitForUserToContinue(PromptMessages.CloseWindowPrompt);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An unexpected error occurred: {ex.Message}");
			}
		}

		// Runs the main game loop. Continuously prompts the user to select a game type and executes the game loop.
		public void RunGameLoop()
		{
			GameTypes selectedGameType;

			do
			{
				selectedGameType = _gameSelector.SelectGameType();

				if (selectedGameType == GameTypes.Quit)
					break;

				if (!InitializeGameFactory(selectedGameType))
				{
					Console.WriteLine("Failed to create game factory. Please try again.");
					continue;
				}

				var (userInterface, gameLogic) = InitializeDependencies();
				var gameFlowController = _factory!.CreateGameFlowController();
				gameFlowController.ExecuteGameLoop(userInterface, gameLogic);
			} while (selectedGameType != GameTypes.Quit);
		}

		// Initializes the game factory based on the selected game type.
		public bool InitializeGameFactory(GameTypes gameType)
		{
			_factory = _factoryCreator.CreateGameFactory(gameType);
			return _factory != null;
		}

		// Initializes and returns the user interface and game logic dependencies.
		public (IConsoleUI, IGameLogic) InitializeDependencies()
		{
			var dependencyInitializer = _factory!.CreateDependencyInitializer();
			return dependencyInitializer.InitializeDependencies();
		}

		// Public method to access the private _factory field for testing purposes.
		public IGameFactory? GetFactory() => _factory;
	}
}