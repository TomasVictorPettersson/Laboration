namespace Laboration.PlayerData.Interfaces
{
	// Defines the contract for managing player data in a game.
	public interface IPlayerData
	{
		// Gets the player's username.
		string UserName { get; }

		// Gets the total number of games played by the player.
		int TotalGamesPlayed { get; }

		// Gets the total number of guesses made by the player.
		int TotalGuesses { get; }

		// Adds the specified number of guesses to the total and increments the games played count.
		void AddGuess(int guesses);

		// Calculates and returns the average number of guesses per game.
		double CalculateAverageGuesses();

		// Determines if the specified object is equal to the current player data.
		bool Equals(object? obj);

		// Returns the hash code for the current player data.
		int GetHashCode();
	}
}