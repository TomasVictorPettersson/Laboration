using Laboration.Business.Classes;
using Laboration.Business.Interfaces;
using Laboration.Common.Classes;
using Laboration.Common.Interfaces;
using Laboration.UI.Classes;
using Laboration.UI.Interfaces;

namespace Laboration.Controllers.Classes
{
	// Controls the flow of the game including initialization, execution, and error handling.
	public static class GameFlowController
	{
		public static void Main()
		{
			// Initialize necessary dependencies for the game.
			IUserInterface userInterface = new UserInterface();
			IHighScoreManager highScoreManager = new HighScoreManager();
			GameConfig gameConfig = new();
			IGameLogic gameLogic = new GameLogic(highScoreManager, userInterface, gameConfig);

			try
			{
				ExecuteGameLoop(userInterface, gameLogic);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}

		// Executes the main game loop with the given user interface and game logic.
		public static void ExecuteGameLoop(IUserInterface userInterface, IGameLogic gameLogic)
		{
			string userName;
			try
			{
				userName = userInterface.GetUserName();
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
			} while (userInterface.AskToContinue());

			Console.WriteLine("Thank you for playing Bulls and Cows!");
		}
	}
}