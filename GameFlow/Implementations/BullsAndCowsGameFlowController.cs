using Laboration.ConsoleUI.Interfaces;
using Laboration.GameFlow.Interfaces;
using Laboration.GameLogic.Interfaces;

namespace Laboration.GameFlow.Implementations
{
	// Handles the initialization of dependencies specifically for the Bulls and Cows game.
	public class BullsAndCowsGameFlowController : IGameFlowController
	{
		// Executes the main game loop with the given user interface and game logic.
		public void ExecuteGameLoop(IConsoleUI consoleUI, IGameLogic gameLogic)
		{
			string userName;
			try
			{
				userName = consoleUI.GetUserName();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error getting user name: {ex.Message}");
				return;
			}

			do
			{
				try
				{
					gameLogic.PlayGame(userName);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error during game play: {ex.Message}");
					return;
				}
			} while (consoleUI.AskToContinue());

			consoleUI.DisplayGoodbyeMessage(userName);
		}
	}
}