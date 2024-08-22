using Laboration.ConsoleUI.GameSelection;
using Laboration.ConsoleUI.Interfaces;
using Laboration.GameFactory.Creators;
using Laboration.GameFactory.Interfaces;
using Laboration.GameLogic.Interfaces;
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
				Console.WriteLine("Thank you for playing!");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An unexpected error occurred: {ex.Message}");
			}
		}

		private static void InitializeGameSelector()
		{
			GameSelector = new GameSelector();
		}

		private static void RunGameLoop()
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

		private static bool InitializeGameFactory(GameTypes gameType)
		{
			Factory = FactoryCreator.CreateFactory(gameType);
			return Factory != null;
		}

		private static (IConsoleUI, IGameLogic) InitializeDependencies()
		{
			var dependencyInitializer = Factory!.CreateDependencyInitializer();
			return dependencyInitializer.InitializeDependencies();
		}
	}
}