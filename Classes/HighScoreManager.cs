namespace Laboration.Classes
{
	public class HighScoreManager
	{
		private readonly string[] separator = ["#&#"];

		private List<PlayerData> ReadHighScoreResultsFromFile()
		{
			List<PlayerData> results = [];
			using StreamReader input = new("result.txt");
			string line;
			while ((line = input.ReadLine()!) != null)
			{
				string[] userNameAndUserScore = line.Split(separator, StringSplitOptions.None);
				string userName = userNameAndUserScore[0];
				int guesses = Convert.ToInt32(userNameAndUserScore[1]);
				PlayerData pd = new(userName, guesses);
				int pos = results.FindIndex(p => p.Equals(pd));
				if (pos < 0)
				{
					results.Add(pd);
				}
				else
				{
					results[pos].AddGuess(guesses);
				}
			}
			return results;
		}

		private static void SortAndDisplayHighScoreList(List<PlayerData> results, string currentUserName)
		{
			results.Sort((p1, p2) => p1.CalculateAverageGuesses().CompareTo(p2.CalculateAverageGuesses()));
			DisplayHighScoreListHeader();
			DisplayHighScoreListResults(results, currentUserName);
		}

		private static void DisplayHighScoreListHeader()
		{
			Console.Clear();
			Console.WriteLine("=== High Score List ===");
			Console.WriteLine("Rank     Player     Games     Average Guesses");
			Console.WriteLine("---------------------------------------------");
		}

		private static void DisplayHighScoreListResults(List<PlayerData> results, string currentUserName)
		{
			int rank = 1;
			foreach (PlayerData p in results)
			{
				bool isCurrentUser = p.UserName.Equals(currentUserName);
				DisplayRank(rank, isCurrentUser);
				DisplayPlayerData(p, isCurrentUser);
				rank++;
			}
		}

		private static void DisplayRank(int rank, bool isCurrentUser)
		{
			if (isCurrentUser)
			{
				Console.ForegroundColor = ConsoleColor.Green;
			}
			Console.Write($"{rank,-4}");
			Console.ResetColor();
		}

		private static void DisplayPlayerData(PlayerData player, bool isCurrentUser)
		{
			Console.ForegroundColor = isCurrentUser ? ConsoleColor.Green : ConsoleColor.White;
			Console.WriteLine($"{player.UserName,10}{player.TotalGamesPlayed,10}" +
				$"{player.CalculateAverageGuesses(),14:F2}");
			Console.ResetColor();
		}

		public void ShowHighScoreList(string currentUserName)
		{
			List<PlayerData> results = ReadHighScoreResultsFromFile();
			SortAndDisplayHighScoreList(results, currentUserName);
		}
	}
}