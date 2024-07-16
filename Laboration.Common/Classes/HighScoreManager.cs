using Laboration.Common.Interfaces;
using Laboration.Data.Classes;
using Laboration.Data.Interfaces;

namespace Laboration.Common.Classes
{
	// Manages high score data, including saving, reading, parsing, updating, sorting, and displaying.
	public class HighScoreManager : IHighScoreManager
	{
		private readonly string[] separator = ["#&#"];

		public void SaveResult(string userName, int numberOfGuesses)
		{
			try
			{
				using StreamWriter output = new("result.txt", append: true);
				output.WriteLine($"{userName}#&#{numberOfGuesses}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error saving result to file: {ex.Message}");
				throw;
			}
		}

		public List<IPlayerData> ReadHighScoreResultsFromFile()
		{
			List<IPlayerData> results = [];
			try
			{
				using StreamReader input = new("result.txt");
				string line;
				while ((line = input.ReadLine()!) != null)
				{
					IPlayerData playerData = ParseLineToPlayerData(line);
					results = UpdateResultsList(results, playerData);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error reading high score results from file: {ex.Message}");
				throw;
			}
			return results;
		}

		public IPlayerData ParseLineToPlayerData(string line)
		{
			try
			{
				string[] userNameAndUserScore = line.Split(separator, StringSplitOptions.None);
				string userName = userNameAndUserScore[0];
				int guesses = Convert.ToInt32(userNameAndUserScore[1]);
				return new PlayerData(userName, guesses);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error parsing line to player data: {ex.Message}");
				throw;
			}
		}

		public List<IPlayerData> UpdateResultsList(List<IPlayerData> results, IPlayerData playerData)
		{
			try
			{
				int pos = results.FindIndex(p => p.Equals(playerData));
				if (pos < 0)
				{
					results.Add(playerData);
				}
				else
				{
					results[pos].AddGuess(playerData.TotalGuesses);
				}
				return results;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating results list: {ex.Message}");
				throw;
			}
		}

		public void SortAndDisplayHighScoreList(List<IPlayerData> results, string currentUserName)
		{
			try
			{
				results.Sort((p1, p2) => p1.CalculateAverageGuesses().CompareTo(p2.CalculateAverageGuesses()));
				DisplayHighScoreListHeader();
				DisplayHighScoreListResults(results, currentUserName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error sorting and displaying high score list: {ex.Message}");
				throw;
			}
		}

		public void DisplayHighScoreListHeader()
		{
			Console.WriteLine("\n=== High Score List ===");
			Console.WriteLine("Rank     Player     Games     Average Guesses");
			Console.WriteLine("---------------------------------------------");
		}

		public void DisplayHighScoreListResults(List<IPlayerData> results, string currentUserName)
		{
			try
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
			catch (Exception ex)
			{
				Console.WriteLine($"Error displaying high score list results: {ex.Message}");
				throw;
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
			try
			{
				List<IPlayerData> results = ReadHighScoreResultsFromFile();
				SortAndDisplayHighScoreList(results, currentUserName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error showing high score list: {ex.Message}");
				throw;
			}
		}
	}
}