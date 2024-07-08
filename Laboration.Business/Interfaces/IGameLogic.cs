namespace Laboration.Business.Interfaces
{
	public interface IGameLogic
	{
		void DisplayWelcomeMessage(string userName);

		void PlayGame(string userName);

		string ProcessGuess(string secretNumber, ref int numberOfGuesses);

		string MakeSecretNumber();

		string GenerateBullsAndCowsFeedback(string secretNumber, string guess);
	}
}