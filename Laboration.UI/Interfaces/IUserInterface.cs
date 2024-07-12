namespace Laboration.UI.Interfaces
{
	public interface IUserInterface
	{
		string GetUserName();

		void DisplayWelcomeMessage(string userName);

		string GetInputFromUser(string prompt);

		bool IsInputValid(string input);

		string GetValidGuessFromUser(int maxRetries);

		void DisplayCorrectMessage(string secretNumber, int numberOfGuesses);

		bool AskToContinue();
	}
}