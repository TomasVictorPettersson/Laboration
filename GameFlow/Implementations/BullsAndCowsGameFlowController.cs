using Laboration.ConsoleUI.Interfaces;
using Laboration.GameFlow.Interfaces;
using Laboration.GameLogic.Interfaces;

namespace Laboration.GameFlow.Implementations
{
	// Manages the game flow for the Bulls and Cows game.
	public class BullsAndCowsGameFlowController : IGameFlowController
	{
		// Executes the main game loop with the provided user interface and game logic.
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
				return; // Exit if user name cannot be retrieved
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
					return; // Exit if an error occurs during game play
				}
			} while (consoleUI.AskToContinue()); // Continue if user wants to play again
			consoleUI.DisplayGoodbyeMessage(userName);
		}
	}
}