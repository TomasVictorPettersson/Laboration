using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.PlayerData.Implementations;
using Laboration.PlayerData.Interfaces;

namespace Laboration.HighScoreManagement.Implementations
{
	// Abstract base class for managing high scores across different game types.
	// Provides methods to save results, read and parse high score data from a file,
	// update the high score list, and sort the results based on the average number of guesses.
	public abstract class HighScoreManagerBase(GameTypes gameType) : IHighScoreManager
	{
		// Game type for which the high score manager is responsible.
		protected readonly GameTypes GameType = gameType;

		// Saves a user's result (name and number of guesses) to a file.
		public void SaveResult(string userName, int numberOfGuesses)
		{
			try
			{
				using StreamWriter output = new(GetFilePath(), append: true);
				output.WriteLine($"{userName}{FileConstants.Separator}{numberOfGuesses}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error saving result to file: {ex.Message}");
				throw;
			}
		}

		// Abstract method to get the file path for saving/loading high scores.
		// Must be implemented in derived classes.
		public abstract string GetFilePath();

		// Reads high score results from the file and returns them as a list of player data.
		public List<IPlayerData> ReadHighScoreResultsFromFile()
		{
			var results = new List<IPlayerData>();
			try
			{
				var filePath = GetFilePath();
				if (File.Exists(filePath))
				{
					using StreamReader input = new(filePath);
					string line;
					while ((line = input.ReadLine()!) != null)
					{
						IPlayerData playerData = ParseLineToPlayerData(line);
						results = UpdateResultsList(results, playerData);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error reading high score results from file: {ex.Message}");
			}
			return results;
		}

		// Parses a line of text into a player data object.
		// Expects format: "userName{Separator}numberOfGuesses".
		public IPlayerData ParseLineToPlayerData(string line)
		{
			try
			{
				string[] userNameAndUserScore = line.Split(FileConstants.Separator, StringSplitOptions.None);
				if (userNameAndUserScore.Length != 2)
				{
					throw new FormatException("The data format is invalid.");
				}

				string userName = userNameAndUserScore[0];
				if (!int.TryParse(userNameAndUserScore[1], out int guesses))
				{
					throw new FormatException("The number of guesses is not a valid integer.");
				}

				return new GamePlayerData(userName, guesses);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error parsing line to player data: {ex.Message}");
				throw;
			}
		}

		// Updates the list of results with a new player's data.
		// Adds the player if not present, or updates existing data.
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

		// Sorts the high score list by the average number of guesses.
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
	}
}