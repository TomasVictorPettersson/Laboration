namespace Laboration.GameResources.Constants
{
	// Contains messages used to prompt user actions in the game.
	// These messages are displayed to guide the user through different stages of interaction.
	public static class PromptMessages
	{
		// Message displayed when the user has read the game instructions and is ready to start.
		// Prompts the user to press any key to begin playing.
		public const string InstructionsReadMessage = "You’ve read the game instructions. Press any key to start playing...";

		// Message prompting the user to start the game.
		// This message appears before the game begins, instructing the user to press any key to start.
		public const string StartPlayingPrompt = "Press any key to start playing...";

		// Message displayed when the game is finished.
		// Congratulates the user and prompts them to press any key to view their high score.
		public const string FinishGamePrompt = "\nCongratulations on finishing the game! Press any key to see your high score...";

		// Message prompting the user to continue to the next stage or action.
		// This appears when the game or application is waiting for further user input.
		public const string ContinuePrompt = "\nPress any key to continue...";

		// Message instructing the user to close the window.
		// Provides a prompt to press any key to close the application window.
		public const string CloseWindowPrompt = "\nPress any key to close this window...";

		// Message displayed to prompt the user to press any key to return to the main menu.
		public const string PressAnyKeyToReturn = "Press any key to return to the main menu...";
	}
}