using Laboration.Business.Classes;
using Laboration.Business.Interfaces;
using Laboration.Common.Classes;
using Laboration.Common.Interfaces;
using Laboration.UI.Classes;
using Laboration.UI.Interfaces;

namespace Laboration.Controllers.Classes
{
	public static class GameFlowController
	{
		public static void Main()
		{
			IUserInterface userInterface = new UserInterface();
			IHighScoreManager highScoreManager = new HighScoreManager();
			IGameLogic gameLogic = new GameLogic(highScoreManager, userInterface);

			ExecuteGameLoop(userInterface, gameLogic);
		}

		public static void ExecuteGameLoop(IUserInterface userInterface, IGameLogic gameLogic)
		{
			var userName = userInterface.GetUserName();

			do
			{
				gameLogic.PlayGame(userName);
			} while (userInterface.AskToContinue());

			Console.WriteLine("Thank you for playing Bulls and Cows!");
		}
	}
}