using System;

namespace Laboration.UI.Interfaces
{
	public interface IUserInterface
	{
		string GetUserName();

		void DisplayWelcomeMessage(string userName);

		string GetValidGuessFromUser(int maxRetries);

		void DisplayCorrectMessage(string secretNumber, int numberOfGuesses);

		bool AskToContinue();
	}
}