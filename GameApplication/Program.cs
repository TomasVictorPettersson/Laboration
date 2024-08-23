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
		public static IGameFactory? Factory { get; set; }
		public static IGameSelector? GameSelector { get; set; }

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

		public static void InitializeGameSelector()
		{
			GameSelector = new GameSelector();
		}

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

		public static bool InitializeGameFactory(GameTypes gameType)
		{
			Factory = FactoryCreator.CreateFactory(gameType);
			return Factory != null;
		}

		public static (IConsoleUI, IGameLogic) InitializeDependencies()
		{
			var dependencyInitializer = Factory!.CreateDependencyInitializer();
			return dependencyInitializer.InitializeDependencies();
		}
	}
}