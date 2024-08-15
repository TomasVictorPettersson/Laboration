using Laboration.PlayerData.Interfaces;

namespace Laboration.HighScoreManagement.Interfaces
{
	// Defines the contract for managing high scores in the game.
	public interface IHighScoreManager
	{
		// Saves a user's game result to a file with their username and number of guesses.
		void SaveResult(string userName, int numberOfGuesses);

		// Reads high score results from a file and returns them as a list of player data.
		List<IPlayerData> ReadHighScoreResultsFromFile();

		// Parses a line of text from the file to create a player data object.
		IPlayerData ParseLineToPlayerData(string line);

		// Updates the high score list with a new player's data or modifies existing data.
		List<IPlayerData> UpdateResultsList(List<IPlayerData> results, IPlayerData playerData);

		// Sorts the high score list by the average number of guesses in ascending order.
		void SortHighScoreList(List<IPlayerData> results);
	}
}