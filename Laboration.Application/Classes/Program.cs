using Laboration.DependencyInjection.Classes;
using Laboration.GameFlow.Classes;
using Laboration.GameFlow.Interfaces;

namespace Laboration.Application
{
	public static class Program
	{
		public static void Main()
		{
			// Create an instance of DependencyInitializer
			var dependencyInitializer = new DependencyInitializer();
			var (userInterface, gameLogic) = dependencyInitializer.InitializeDependencies();

			// Assuming IGameFlowController is an interface, and you need to provide its implementation
			IGameFlowController gameFlowController = new GameFlowController();

			try
			{
				gameFlowController.ExecuteGameLoop(userInterface, gameLogic);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}
	}
}