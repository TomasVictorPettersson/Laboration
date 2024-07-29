using Laboration.PlayerData.Interfaces;

namespace Laboration.PlayerData.Implementations
{
	// Represents player data for the Bulls and Cows game, including username,
	// total games played, and total guesses made.
	public class BullsAndCowsPlayerData : IPlayerData
	{
		// Properties
		public string UserName { get; }

		public int TotalGamesPlayed { get; private set; } = 0;
		public int TotalGuesses { get; private set; }

		// Constructor
		public BullsAndCowsPlayerData(string userName, int guesses)
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
			TotalGamesPlayed = 1; // Initialize to 1 if this is the player's first game
		}

		// Adds the specified number of guesses to the player's total and increments games played.
		public void AddGuess(int guesses)
		{
			if (guesses < 0)
			{
				throw new ArgumentException("Number of guesses cannot be negative.", nameof(guesses));
			}

			TotalGuesses += guesses;
			TotalGamesPlayed++;
		}

		// Calculates and returns the average number of guesses per game played by the player.
		public double CalculateAverageGuesses()
		{
			if (TotalGamesPlayed == 0)
			{
				throw new InvalidOperationException("Cannot calculate average guesses when no games have been played.");
			}

			return (double)TotalGuesses / TotalGamesPlayed;
		}

		// Overrides Equals method to compare BullsAndCowsPlayerData objects based on username.
		public override bool Equals(object? obj)
		{
			return obj is BullsAndCowsPlayerData other && UserName.Equals(other.UserName, StringComparison.OrdinalIgnoreCase);
		}

		// Overrides GetHashCode method to generate hash code based on username.
		public override int GetHashCode()
		{
			return UserName.GetHashCode(StringComparison.OrdinalIgnoreCase);
		}
	}
}