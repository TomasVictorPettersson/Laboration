namespace Laboration.Business.Interfaces
{
	public interface IGameLogic
	{
		void InitializeGame(string userName);

		void PlayGame(string userName);

		void DisplaySecretNumberForPractice(string secretNumber);

		void PlayGameLoop(string secretNumber, string userName);

		void EndGame(string secretNumber, string userName, int numberOfGuesses);

		string ProcessGuess(string secretNumber, ref int numberOfGuesses);

		string MakeSecretNumber();
	}
}