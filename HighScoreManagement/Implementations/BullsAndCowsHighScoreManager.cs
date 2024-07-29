﻿using Laboration.HighScoreManagement.Interfaces;
using Laboration.PlayerData.Implementations;
using Laboration.PlayerData.Interfaces;

namespace Laboration.HighScoreManagement.Implementations
{
	// Manages high score data for the Bulls and Cows game, including saving results,
	// reading from a file, parsing player data, updating the results list, sorting, and displaying.
	public class BullsAndCowsHighScoreManager : IHighScoreManager
	{
		private readonly string[] separator = ["#&#"];

		// Saves a user's result to a file.
		public void SaveResult(string userName, int numberOfGuesses)
		{
			try
			{
				using StreamWriter output = new("result.txt", append: true);
				output.WriteLine($"{userName}{separator[0]}{numberOfGuesses}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error saving result to file: {ex.Message}");
				throw;
			}
		}

		// Reads high score results from a file and returns a list of player data.
		public List<IPlayerData> ReadHighScoreResultsFromFile()
		{
			var results = new List<IPlayerData>();
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

		// Parses a line of text to create a player data object.
		public IPlayerData ParseLineToPlayerData(string line)
		{
			try
			{
				string[] userNameAndUserScore = line.Split(separator, StringSplitOptions.None);
				string userName = userNameAndUserScore[0];
				int guesses = Convert.ToInt32(userNameAndUserScore[1]);
				return new BullsAndCowsPlayerData(userName, guesses);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error parsing line to player data: {ex.Message}");
				throw;
			}
		}

		// Updates the results list with a new player's data or updates existing data.
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

		// Reads the high score results from the file, sorts them, and displays the high score list.
		public void DisplayHighScoreList(string currentUserName)
		{
			try
			{
				var results = ReadHighScoreResultsFromFile();
				SortHighScoreList(results);
				RenderHighScoreList(results, currentUserName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error showing high score list: {ex.Message}");
				throw;
			}
		}

		// Sorts the high score list based on the average number of guesses.
		public void SortHighScoreList(List<IPlayerData> results)
		{
			try
			{
				results.Sort((p1, p2) => p1.CalculateAverageGuesses().CompareTo(p2.CalculateAverageGuesses()));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error sorting high score list: {ex.Message}");
				throw;
			}
		}

		// Displays the high score list with headers and formatted player data.
		public void RenderHighScoreList(List<IPlayerData> results, string currentUserName)
		{
			try
			{
				var (maxUserNameLength, totalWidth) = CalculateDisplayDimensions(results);

				DisplayHighScoreListHeader(maxUserNameLength, totalWidth);
				PrintHighScoreResults(results, currentUserName, maxUserNameLength);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error displaying high score list: {ex.Message}");
				throw;
			}
		}

		// Calculates the maximum username length and total display width for formatting.
		public (int maxUserNameLength, int totalWidth) CalculateDisplayDimensions(List<IPlayerData> results)
		{
			int maxUserNameLength = results.Max(p => p.UserName.Length);
			int totalWidth = 6 + maxUserNameLength + 8 + 15 + 3;
			return (maxUserNameLength, totalWidth);
		}

		// Displays the header for the high score list.
		public void DisplayHighScoreListHeader(int maxUserNameLength, int totalWidth)
		{
			const string header = "=== High Score List ===";
			int leftPadding = (totalWidth - header.Length) / 2;
			Console.WriteLine($"\n{new string(' ', leftPadding)}{header}");
			Console.WriteLine($"{"Rank",-6} {"Player".PadRight(maxUserNameLength)} {"Games",-8} {"Average Guesses",-15}");
			Console.WriteLine(new string('-', totalWidth));
		}

		// Displays the list of player data in a formatted manner.
		public void PrintHighScoreResults(List<IPlayerData> results, string currentUserName, int maxUserNameLength)
		{
			try
			{
				int rank = 1;
				foreach (IPlayerData p in results)
				{
					bool isCurrentUser = p.UserName.Equals(currentUserName);
					DisplayRank(rank, isCurrentUser);
					DisplayPlayerData(p, isCurrentUser, maxUserNameLength);
					rank++;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error displaying high score list results: {ex.Message}");
				throw;
			}
		}

		// Displays the rank of the player, highlighting the current user if necessary.
		public void DisplayRank(int rank, bool isCurrentUser)
		{
			if (isCurrentUser)
			{
				Console.ForegroundColor = ConsoleColor.Green;
			}
			Console.Write($"{rank,-6}");
			Console.ResetColor();
		}

		// Displays the player data, highlighting the current user if necessary.
		public void DisplayPlayerData(IPlayerData player, bool isCurrentUser, int maxUserNameLength)
		{
			Console.ForegroundColor = isCurrentUser ? ConsoleColor.Green : ConsoleColor.White;
			Console.WriteLine($"{player.UserName.PadRight(maxUserNameLength)} {player.TotalGamesPlayed,8} {player.CalculateAverageGuesses(),15:F2}");
			Console.ResetColor();
		}
	}
}