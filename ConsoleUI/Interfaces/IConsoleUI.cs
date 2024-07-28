namespace Laboration.ConsoleUI.Interfaces
{
	public interface IConsoleUI
	{
		string GetUserName();

		void ValidateUserName(string userName);

		bool IsValidUserName(string userName);

		void DisplayWelcomeMessage(string userName);

		string GetInputFromUser(string prompt);

		bool IsInputValid(string input);

		string GetValidGuessFromUser();

		void DisplayCorrectMessage(string secretNumber, int numberOfGuesses);

		bool AskToContinue();

		void DisplayGoodbyeMessage(string userName);
	}
}