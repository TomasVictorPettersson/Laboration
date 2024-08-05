using Laboration.PlayerData.Interfaces;

namespace Laboration.ConsoleUI.Interfaces
{
	// Defines the contract for user interaction components in a console-based game.
	public interface IConsoleUI
	{
		// Prompts the user to enter their username.
		string GetUserName();

		// Clears the console screen.
		void ClearConsole();

		// Displays a welcome message to the user, explaining the game rules.
		void DisplayWelcomeMessage(string userName);

		// Displays the secret number for practice mode.
		void DisplaySecretNumberForPractice(string secretNumber);

		// Gets input from the user.
		string GetInputFromUser();

		// Prompts the user to enter a valid 4-digit guess.
		string GetValidGuessFromUser();

		// Displays feedback for the player's guess.

		void DisplayGuessFeedback(string guessFeedback);

		// Displays a message indicating the correct number and the number of guesses taken.
		void DisplayCorrectMessage(string secretNumber, int numberOfGuesses);

		// Displays the high score list with formatted player data and highlights the current user.
		void DisplayHighScoreList(string currentUserName);

		// Displays the high score list with headers and formatted player data.
		void RenderHighScoreList(List<IPlayerData> results, string currentUserName);

		// Calculates the maximum username length and total display width for formatting.
		(int maxUserNameLength, int totalWidth) CalculateDisplayDimensions(List<IPlayerData> results);

		// Displays the header for the high score list with proper formatting.
		void DisplayHighScoreListHeader(int maxUserNameLength, int totalWidth);

		// Displays the list of player data in a formatted manner, highlighting the current user.
		void PrintHighScoreResults(List<IPlayerData> results, string currentUserName, int maxUserNameLength);

		// Sets console color based on whether the player is the current user.
		void SetConsoleColor(bool isCurrentUser);

		// Displays the rank of the player.
		void DisplayRank(int rank);

		// Displays detailed player data, with special formatting for the current user.
		void DisplayPlayerData(IPlayerData player, bool isCurrentUser, int maxUserNameLength);

		// Asks the user if they want to continue playing or exit.
		bool AskToContinue();

		// Displays a farewell message to the user after the game ends.
		void DisplayGoodbyeMessage(string userName);
	}
}