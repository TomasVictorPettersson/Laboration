using Laboration.PlayerData.Interfaces;

namespace Laboration.HighScoreManagement.Interfaces
{
	// Defines the contract for managing high scores in the Bulls and Cows game.

	public interface IHighScoreManager
	{
		// Saves a user's game result to a file.

		void SaveResult(string userName, int numberOfGuesses);

		// Reads high score results from a file and returns a list of player data.

		List<IPlayerData> ReadHighScoreResultsFromFile();

		// Parses a line of text from the file to create a player data object.

		IPlayerData ParseLineToPlayerData(string line);

		// Updates the results list with a new player's data or updates existing data.

		List<IPlayerData> UpdateResultsList(List<IPlayerData> results, IPlayerData playerData);

		// Sorts the high score list based on the average number of guesses.

		void SortHighScoreList(List<IPlayerData> results);

		// Calculates the maximum username length and total display width for formatting the high score list.

		(int maxUserNameLength, int totalWidth) CalculateDisplayDimensions(List<IPlayerData> results);
	}
}