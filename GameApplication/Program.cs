using Laboration.ConsoleUI.Interfaces;
using Laboration.GameFactory.Creators;
using Laboration.GameFactory.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameApplication
{
	public static class Program
	{
		public static IGameFactory? Factory { get; set; }
		public static IGameSelector? GameSelector { get; set; }

		public static void Main()
		{
			GameTypes selectedGameType;

			do
			{
				selectedGameType = GameSelector?.SelectGameType() ?? GameTypes.Quit;

				if (selectedGameType == GameTypes.Quit)
					break;

				Factory = FactoryCreator.CreateFactory(selectedGameType);

				if (Factory == null)
				{
					Console.WriteLine("Game initialization failed. Please try again.");
					continue;
				}

				var dependencyInitializer = Factory.CreateDependencyInitializer();
				var (userInterface, gameLogic) = dependencyInitializer.InitializeDependencies();

				try
				{
					var gameFlowController = Factory.CreateGameFlowController();
					gameFlowController.ExecuteGameLoop(userInterface, gameLogic);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"An error occurred: {ex.Message}");
				}
			}
			while (selectedGameType != GameTypes.Quit);

			Console.WriteLine("Thank you for playing!");
		}
	}
}