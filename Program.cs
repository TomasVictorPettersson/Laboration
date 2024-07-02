namespace MooGame
{
	internal static class Program
	{
		public static void Main()
		{
			string userName = GameLogic.GetUserName();
			bool playOn = true;
			while (playOn)
			{
				GameLogic.PlayGame(userName);
				playOn = GameLogic.AskToContinue();
			}
		}
	}
}