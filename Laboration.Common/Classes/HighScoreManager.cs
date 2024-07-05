using Laboration.Common.Interfaces;
using Laboration.Data.Classes;
using Laboration.Data.Interfaces;

namespace Laboration.Common.Classes
{
	public class HighScoreManager : IHighScoreManager
	{
		private readonly string[] separator = ["#&#"];

		public List<IPlayerData> ReadHighScoreResultsFromFile()
		{
			List<IPlayerData> results = [];
			using StreamReader input = new("result.txt");
			string line;
			while ((line = input.ReadLine()!) != null)
			{
				string[] userNameAndUserScore = line.Split(separator, StringSplitOptions.None);
				string userName = userNameAndUserScore[0];
				int guesses = Convert.ToInt32(userNameAndUserScore[1]);
				IPlayerData pd = new PlayerData(userName, guesses);
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

		public void SortAndDisplayHighScoreList(List<IPlayerData> results, string currentUserName)
		{
			results.Sort((p1, p2) => p1.CalculateAverageGuesses().CompareTo(p2.CalculateAverageGuesses()));
			DisplayHighScoreListHeader();
			DisplayHighScoreListResults(results, currentUserName);
		}

		public void DisplayHighScoreListHeader()
		{
			Console.Clear();
			Console.WriteLine("=== High Score List ===");
			Console.WriteLine("Rank     Player     Games     Average Guesses");
			Console.WriteLine("---------------------------------------------");
		}

		public void DisplayHighScoreListResults(List<IPlayerData> results, string currentUserName)
		{
			int rank = 1;
			foreach (IPlayerData p in results)
			{
				bool isCurrentUser = p.UserName.Equals(currentUserName);
				DisplayRank(rank, isCurrentUser);
				DisplayPlayerData(p, isCurrentUser);
				rank++;
			}
		}

		public void DisplayRank(int rank, bool isCurrentUser)
		{
			if (isCurrentUser)
			{
				Console.ForegroundColor = ConsoleColor.Green;
			}
			Console.Write($"{rank,-4}");
			Console.ResetColor();
		}

		public void DisplayPlayerData(IPlayerData player, bool isCurrentUser)
		{
			Console.ForegroundColor = isCurrentUser ? ConsoleColor.Green : ConsoleColor.White;
			Console.WriteLine($"{player.UserName,10}{player.TotalGamesPlayed,10}" +
				$"{player.CalculateAverageGuesses(),14:F2}");
			Console.ResetColor();
		}

		public void ShowHighScoreList(string currentUserName)
		{
			List<IPlayerData> results = ReadHighScoreResultsFromFile();
			SortAndDisplayHighScoreList(results, currentUserName);
		}
	}
}