namespace Laboration.Business.Interfaces
{
	public interface IGameLogic
	{
		void PlayGame(string userName);

		void InitializeGame(string userName);

		string MakeSecretNumber();

		void DisplaySecretNumberForPractice(string secretNumber);

		void PlayGameLoop(string secretNumber, string userName);

		bool IsCorrectGuess(string guess, string secretNumber);

		string ProcessGuess(string secretNumber, ref int numberOfGuesses);

		void EndGame(string secretNumber, string userName, int numberOfGuesses);
	}
}