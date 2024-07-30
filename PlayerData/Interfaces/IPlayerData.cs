namespace Laboration.PlayerData.Interfaces
{
	// Represents player data for the Bulls and Cows game.

	public interface IPlayerData
	{
		// Gets the username of the player.

		string UserName { get; }

		// Gets the total number of games played by the player.

		int TotalGamesPlayed { get; }

		// Gets the total number of guesses made by the player.

		int TotalGuesses { get; }

		// Adds the specified number of guesses to the player's total and increments the number of games played.

		void AddGuess(int guesses);

		// Calculates and returns the average number of guesses per game played by the player.

		double CalculateAverageGuesses();

		// Determines whether the specified object is equal to the current player data.

		bool Equals(object? obj);

		// Gets the hash code for the current player data.

		int GetHashCode();
	}
}