using Laboration.Common.Classes;
using Laboration.Common.Interfaces;
using Laboration.Interfaces;
using Laboration.UI.Classes;
using Laboration.UI.Interfaces;

namespace Laboration.Classes
{
	public static class Program
	{
		public static void Main()
		{
			IUserInterface userInterface = new UserInterface();
			IHighScoreManager highScoreManager = new HighScoreManager();
			IGameLogic gameLogic = new GameLogic(highScoreManager, userInterface);
			string userName = userInterface.GetUserName();
			bool playOn = true;
			while (playOn)
			{
				gameLogic.PlayGame(userName);
				playOn = userInterface.AskToContinue();
			}
		}
	}
}