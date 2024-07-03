namespace MooGame
{
	internal static class Program
	{
		public static void Main()
		{
			string userName = UserInterface.GetUserName();
			bool playOn = true;
			while (playOn)
			{
				GameLogic.PlayGame(userName);
				playOn = UserInterface.AskToContinue();
			}
		}
	}
}