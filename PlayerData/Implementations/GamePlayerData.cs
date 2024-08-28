using PlayerData.Interfaces;

namespace PlayerData.Implementations
{
	// Represents player data, including username, total games played, and total guesses made.
	public class GamePlayerData : IPlayerData
	{
		// Gets the username of the player.
		public string UserName { get; }

		// Gets the total number of games played by the player.
		public int TotalGamesPlayed { get; private set; } = 0;

		// Gets the total number of guesses made by the player.
		public int TotalGuesses { get; private set; }

		// Constructor to initialize player data with a username and the number of guesses for the first game.
		public GamePlayerData(string userName, int guesses)
		{
			if (string.IsNullOrEmpty(userName))
			{
				throw new ArgumentException("User name cannot be null or empty.", nameof(userName));
			}

			if (guesses < 0)
			{
				throw new ArgumentException("Number of guesses cannot be negative.", nameof(guesses));
			}

			UserName = userName;
			TotalGuesses = guesses;
			TotalGamesPlayed = 1; // Initialize to 1 if this is the player's first game.
		}

		// Adds the specified number of guesses to the player's total and increments the games played count.
		public void AddGuess(int guesses)
		{
			if (guesses < 0)
			{
				throw new ArgumentException("Number of guesses cannot be negative.", nameof(guesses));
			}

			TotalGuesses += guesses;
			TotalGamesPlayed++;
		}

		// Calculates and returns the average number of guesses per game played.
		public double CalculateAverageGuesses()
		{
			if (TotalGamesPlayed == 0)
			{
				throw new InvalidOperationException("Cannot calculate average guesses when no games have been played.");
			}

			return (double)TotalGuesses / TotalGamesPlayed;
		}

		// Compares this GamePlayerData object to another based on the username.
		public override bool Equals(object? obj)
		{
			return obj is GamePlayerData other && UserName.Equals(other.UserName, StringComparison.OrdinalIgnoreCase);
		}

		// Generates a hash code for this GamePlayerData object based on the username.
		public override int GetHashCode()
		{
			return UserName.GetHashCode(StringComparison.OrdinalIgnoreCase);
		}
	}
}