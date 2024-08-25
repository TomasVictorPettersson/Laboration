using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.ConsoleUI.Utils;
using Laboration.GameFactory.Implementations;
using Laboration.GameFactory.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;

// TODO: ADD comments
namespace Laboration.GameApplication
{
	public class Program(IGameSelector gameSelector, IFactoryCreator factoryCreator)
	{
		private IGameFactory? _factory;
		private readonly IGameSelector _gameSelector = gameSelector;
		private readonly IFactoryCreator _factoryCreator = factoryCreator;

		public static void Main()
		{
			try
			{
				var program = CreateAndInitializeProgram();
				program.RunGameLoop();
				ConsoleUtils.WaitForUserToContinue(PromptMessages.CloseWindowPrompt);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An unexpected error occurred: {ex.Message}");
			}
		}

		// Method to create and initialize Program and its dependencies
		public static Program CreateAndInitializeProgram()
		{
			var gameSelector = new GameSelector();
			var factoryCreator = new FactoryCreator();
			return new Program(gameSelector, factoryCreator);
		}

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

		public bool InitializeGameFactory(GameTypes gameType)
		{
			_factory = _factoryCreator.CreateFactory(gameType);
			return _factory != null;
		}

		public (IConsoleUI, IGameLogic) InitializeDependencies()
		{
			var dependencyInitializer = _factory!.CreateDependencyInitializer();
			return dependencyInitializer.InitializeDependencies();
		}

		// Public method to access private _factory field for testing purposes
		public IGameFactory? GetFactory() => _factory;
	}
}