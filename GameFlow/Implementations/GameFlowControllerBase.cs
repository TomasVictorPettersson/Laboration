using Laboration.ConsoleUI.Interfaces;
using Laboration.GameFlow.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFlow.Implementations
{
	// Abstract base class for managing game flow.
	public abstract class GameFlowControllerBase(GameTypes gameType) : IGameFlowController
	{
		public readonly GameTypes GameType = gameType;

		// Manages the game flow, including user interaction and game loop.
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

			// Displays GoodbyeMessage when exiting the game
			consoleUI.DisplayGoodbyeMessage(GameType, userName);
		}
	}
}