using Laboration.ConsoleUI.Interfaces;
using Laboration.GameFlow.Interfaces;
using Laboration.GameLogic.Interfaces;

namespace Laboration.GameFlow.Implementations
{
	// Manages the game flow for the Bulls and Cows game.
	public class GameFlowController : IGameFlowController
	{
		// Manages the game flow by retrieving the user’s name, running the game loop
		// (starting with a flag for the first game),
		// repeating the loop if the user wants to play again, and showing a goodbye message when finished.
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

			bool isFirstGame = true;

			do
			{
				try
				{
					gameLogic.PlayGame(userName, isFirstGame);
					isFirstGame = false; // Set to false after the first game
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