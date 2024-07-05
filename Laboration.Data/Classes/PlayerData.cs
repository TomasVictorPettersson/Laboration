using Laboration.Data.Interfaces;

namespace Laboration.Data.Classes
{
	public class PlayerData(string userName, int guesses) : IPlayerData
	{
		public string UserName { get; } = userName;
		public int TotalGamesPlayed { get; private set; } = 1;
		private int TotalGuesses = guesses;

		public void AddGuess(int guesses)
		{
			TotalGuesses += guesses;
			TotalGamesPlayed++;
		}

		public double CalculateAverageGuesses()
		{
			return (double)TotalGuesses / TotalGamesPlayed;
		}

		public override bool Equals(object? obj)
		{
			return obj is PlayerData data && UserName.Equals(data.UserName);
		}

		public override int GetHashCode()
		{
			return UserName.GetHashCode();
		}
	}
}