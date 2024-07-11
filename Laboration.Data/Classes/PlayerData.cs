using Laboration.Data.Interfaces;
using System;

namespace Laboration.Data.Classes
{
	// Represents data for a player including username, total games played, and total guesses made.
	public class PlayerData : IPlayerData
	{
		// Properties
		public string UserName { get; }  // Gets the username of the player.

		// Auto-property for total games played by the player.
		public int TotalGamesPlayed { get; private set; } = 1;

		// Auto-property for total number of guesses made by the player.
		public int TotalGuesses { get; private set; }

		// Constructor
		// Initializes a new instance of the PlayerData class with specified username and initial guesses.
		public PlayerData(string userName, int guesses)
		{
			// Validation: Username cannot be null or empty.
			if (string.IsNullOrEmpty(userName))
			{
				throw new ArgumentException("User name cannot be null or empty.", nameof(userName));
			}

			// Validation: Number of guesses cannot be negative.
			if (guesses < 0)
			{
				throw new ArgumentException("Number of guesses cannot be negative.", nameof(guesses));
			}

			UserName = userName;
			TotalGuesses = guesses;
		}

		// Methods

		// Adds the specified number of guesses to the player's total and increments games played.
		public void AddGuess(int guesses)
		{
			// Validation: Number of guesses cannot be negative.
			if (guesses < 0)
			{
				throw new ArgumentException("Number of guesses cannot be negative.", nameof(guesses));
			}

			TotalGuesses += guesses;
			TotalGamesPlayed++;
		}

		// Calculates and returns the average number of guesses per game played by the player.
		// Throws InvalidOperationException if no games have been played yet.
		public double CalculateAverageGuesses()
		{
			// Validation: Cannot calculate average guesses if no games have been played.
			if (TotalGamesPlayed == 0)
			{
				throw new InvalidOperationException("Cannot calculate average guesses when no games have been played.");
			}

			return (double)TotalGuesses / TotalGamesPlayed;
		}

		// Overrides Equals method to compare PlayerData objects based on username.
		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			PlayerData other = (PlayerData)obj;
			return UserName.Equals(other.UserName);
		}

		// Overrides GetHashCode method to generate hash code based on username.
		public override int GetHashCode()
		{
			return UserName.GetHashCode();
		}
	}
}