namespace GameResources.Constants
{
	// Contains messages used to prompt user actions in game.
	// These messages guide the user through different stages of interaction.
	public static class PromptMessages
	{
		// Prompt displayed when the user has finished reading instructions and is ready to start.
		public const string InstructionsReadMessage = "You’ve read the game instructions. Press any key to start playing...";

		// Prompt displayed to start the game.
		public const string StartPlayingPrompt = "Press any key to start playing...";

		// Prompt displayed when the game finishes.
		public const string FinishGamePrompt = "\nCongratulations on finishing the game! Press any key to see your high score...";

		// Prompt displayed when waiting for further user input.
		public const string ContinuePrompt = "\nPress any key to continue...";

		// Prompt displayed to instruct the user to close the application window.
		public const string CloseWindowPrompt = "\nPress any key to close this window...";

		// Prompt displayed to return to the main menu.
		public const string PressAnyKeyToReturn = "Press any key to return to the main menu...";

		// Prompt for the user to enter the game they want to play.
		public const string GameSelectionPrompt = "\nEnter the number of the game you want to play: ";

		// Prompt for the user to enter their username.
		public const string UserNamePrompt = "Enter your username: ";

		// Prompt for the user to enter their 4-digit guess.
		public const string GuessPrompt = "Enter your 4-digit guess: ";

		// Prompt asking the user if they want to play the game again.
		public const string PlayAgainPrompt = "Do you want to play again? (y/n): ";
	}
}