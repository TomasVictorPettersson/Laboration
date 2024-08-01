﻿using Laboration.HighScoreManagement.Interfaces;
using Laboration.PlayerData.Implementations;
using Laboration.PlayerData.Interfaces;

namespace Laboration.HighScoreManagement.Implementations
{
	// Manages high score data for the Bulls and Cows game, including saving results,
	// reading from a file, parsing player data, updating the results list and sorting.
	public class BullsAndCowsHighScoreManager : IHighScoreManager
	{
		private const string Separator = "#&#";
		private const string FilePath = "result.txt";

		// Saves a user's result to a file.
		public void SaveResult(string userName, int numberOfGuesses)
		{
			try
			{
				using StreamWriter output = new(FilePath, append: true);
				output.WriteLine($"{userName}{Separator}{numberOfGuesses}");
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
				using StreamReader input = new(FilePath);
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
				string[] userNameAndUserScore = line.Split(Separator, StringSplitOptions.None);
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

		// Calculates the maximum username length and total display width for formatting.
		public (int maxUserNameLength, int totalWidth) CalculateDisplayDimensions(List<IPlayerData> results)
		{
			int maxUserNameLength = results.Max(p => p.UserName.Length);
			int totalWidth = 6 + maxUserNameLength + 8 + 15 + 3;
			return (maxUserNameLength, totalWidth);
		}
	}
}