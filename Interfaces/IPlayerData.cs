namespace Laboration.Interfaces
{
	public interface IPlayerData
	{
		string UserName { get; }
		int TotalGamesPlayed { get; }

		void AddGuess(int guesses);

		double CalculateAverageGuesses();
	}
}