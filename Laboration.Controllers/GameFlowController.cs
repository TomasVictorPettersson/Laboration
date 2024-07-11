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
		// Entry point of the application.
		public static void Main()
		{
			// Initialize necessary dependencies for the game.
			IUserInterface userInterface = new UserInterface();
			IHighScoreManager highScoreManager = new HighScoreManager();
			GameConfig gameConfig = new();
			IGameLogic gameLogic = new GameLogic(highScoreManager, userInterface, gameConfig);

			try
			{
				// Start the game loop with initialized dependencies.
				ExecuteGameLoop(userInterface, gameLogic);
			}
			catch (Exception ex)
			{
				// Handle any unhandled exceptions during execution.
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}

		// Executes the main game loop with the given user interface and game logic.
		public static void ExecuteGameLoop(IUserInterface userInterface, IGameLogic gameLogic)
		{
			string userName;
			try
			{
				// Get the user's name using the provided user interface.
				userName = userInterface.GetUserName();
			}
			catch (Exception ex)
			{
				// Handle errors when retrieving the user's name.
				Console.WriteLine($"Error getting user name: {ex.Message}");
				return; // Exit the method if getting user name fails
			}

			// Continuously play the game until the user chooses to stop.
			do
			{
				try
				{
					// Start the game logic for the specified user.
					gameLogic.PlayGame(userName);
				}
				catch (Exception ex)
				{
					// Handle errors that occur during game play.
					Console.WriteLine($"Error during game play: {ex.Message}");
					return; // Exit the method if game play fails
				}
			} while (userInterface.AskToContinue());

			// Display a farewell message when the user decides to stop playing.
			Console.WriteLine("Thank you for playing Bulls and Cows!");
		}
	}
}