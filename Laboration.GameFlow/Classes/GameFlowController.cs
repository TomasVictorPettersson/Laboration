using Laboration.GameFlow.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.UI.Interfaces;

namespace Laboration.GameFlow.Classes
{
	// Controls the flow of the game including initialization, execution, and error handling.
	public class GameFlowController : IGameFlowController
	{
		// Executes the main game loop with the given user interface and game logic.
		public void ExecuteGameLoop(IUserInterface userInterface, IGameLogic gameLogic)
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

			userInterface.DisplayGoodbyeMessage(userName);
		}
	}
}