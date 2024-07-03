using Laboration.Interfaces;

namespace Laboration.Classes
{
	internal static class Program
	{
		public static void Main()
		{
			IUserInterface userInterface = new UserInterface();
			string userName = userInterface.GetUserName();
			IGameLogic gameLogic = new GameLogic();
			bool playOn = true;
			while (playOn)
			{
				gameLogic.PlayGame(userName);
				playOn = userInterface.AskToContinue();
			}
		}
	}
}