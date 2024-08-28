using GameResources.Enums;
using PlayerData.Interfaces;

namespace ConsoleUI.Interfaces
{
	// Defines the contract for user interaction components in a console-based game.
	public interface IConsoleUI
	{
		// Prompts the user to enter their username and returns it.
		string GetUserName();

		// Displays a personalized welcome message.
		// Provides a detailed message if it's a new game, or a brief message if returning.
		void DisplayWelcomeMessage(GameTypes gameType, string userName, bool isNewGame);

		// Shows the secret number in practice mode.
		void DisplaySecretNumberForPractice(string secretNumber);

		// Gets user input as a string.
		string GetInputFromUser();

		// Prompts the user to enter a valid 4-digit guess.
		string GetValidGuessFromUser(GameTypes gameType);

		// Provides feedback on the user's guess.
		void DisplayGuessFeedback(string guessFeedback);

		// Shows the correct number and the number of guesses taken.
		void DisplayCorrectMessage(string secretNumber, int numberOfGuesses);

		// Displays the high score list with formatted player data, highlighting the current user.
		void DisplayHighScoreList(string currentUserName);

		// Renders the high score list with headers and formatted player data.
		void RenderHighScoreList(List<IPlayerData> results, string currentUserName);

		// Calculates dimensions for display based on player data.
		(int maxUserNameLength, int totalWidth) CalculateDisplayDimensions(List<IPlayerData> results);

		// Displays the header for the high score list with proper formatting.
		void DisplayHighScoreListHeader(int maxUserNameLength, int totalWidth);

		// Prints the high score results in a formatted manner, highlighting the current user.
		void PrintHighScoreResults(List<IPlayerData> results, string currentUserName, int maxUserNameLength);

		// Sets the console color to indicate if the player is the current user.
		void SetConsoleColor(bool isCurrentUser);

		// Displays the player's rank.
		void DisplayRank(int rank);

		// Shows detailed player data with special formatting for the current user.
		void DisplayPlayerData(IPlayerData player, bool isCurrentUser, int maxUserNameLength);

		// Asks the user if they want to continue playing or exit, returning their choice.
		bool AskToContinue();

		// Displays a personalized goodbye message and prompts the user to close the window.
		void DisplayGoodbyeMessage(GameTypes gameType, string userName);

		// Provides a format for the welcome message based on the game type.
		string GetWelcomeMessageFormat(GameTypes gameType);

		// Provides a format for the goodbye message based on the game type.
		string GetGoodbyeMessageFormat(GameTypes gameType);
	}
}