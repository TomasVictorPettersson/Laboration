namespace Laboration.PlayerData.Interfaces
{
	public interface IPlayerData
	{
		string UserName { get; }
		int TotalGamesPlayed { get; }
		int TotalGuesses { get; }

		void AddGuess(int guesses);

		double CalculateAverageGuesses();

		bool Equals(object? obj);

		int GetHashCode();
	}
}