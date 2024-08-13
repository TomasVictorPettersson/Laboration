namespace Laboration.ConstantsLibrary.Constants
{
	// Contains message-related constants used throughout the application.
	public static class MessageConstants
	{
		// The format string for the welcome message displayed when starting a new game.
		public const string WelcomeMessageFormat =
			"Welcome, {0}, to Bulls and Cows!\n\n" +
			"The goal is to guess a 4-digit number where each digit is unique and between 0 and 9.\n\n" +
			"For each guess, you’ll get feedback in the format ‘BBBB,CCCC’, where:\n" +
			"- ‘BBBB’ is the number of bulls (correct digits in the correct positions).\n" +
			"- ‘CCCC’ is the number of cows (correct digits in the wrong positions).\n\n" +
			"If you get 'No matches found', none of your guessed digits are in the 4-digit number.";

		// The format string for the welcome back message displayed when the player returns.
		public const string WelcomeBackMessageFormat =
			"Welcome back, {0}!\nGlad to see you again. Good luck with your next game!";

		// The prompt message asking the user to enter their username.
		public const string UserNamePrompt = "Enter your username: ";

		// The message displayed when the user input is invalid (e.g., not a 4-digit number with unique digits).
		public const string InvalidInputMessage = "Invalid input. Please enter a 4-digit number with unique digits.\n";

		// Prefix for the feedback message showing results of the user's guess.
		public const string FeedbackPrefix = "Feedback: ";

		// The message displayed in practice mode showing the secret number.
		public const string SecretNumberPracticeMessage = "For practice mode, the 4-digit number is: {0}";

		// The format string for the message displayed when the user correctly guesses the number.
		public const string CorrectGuessMessageFormat =
			"Correct! The 4-digit number was: {0}\nIt took you {1} {2}.";

		// The prompt message asking the user if they want to play again.
		public const string PlayAgainPrompt = "Do you want to play again? (y/n): ";

		// The message displayed when the user input for continuing the game is invalid.
		public const string InvalidPlayAgainResponse = "Invalid input. Please enter y for yes or n for no.\n";

		// The format string for the goodbye message displayed when the user exits the game.
		public const string GoodbyeMessageFormat = "Thank you, {0}, for playing Bulls and Cows!";

		// The prompt message asking the user to enter their 4-digit guess.
		public const string GuessPrompt = "Enter your 4-digit guess: ";

		// The message displayed when no matches are found for the user's guess.
		public const string NoMatchesFoundMessage = "No matches found";

		// The message asking the user to press any key to close the window.
		public const string CloseWindowPrompt = "\nPress any key to close this window...";

		// Singular form of 'guess' for formatting.
		public const string GuessSingular = "guess";

		// Plural form of 'guess' for formatting.
		public const string GuessPlural = "guesses";

		// The header for the rank column in the high score list.
		public const string RankHeader = "Rank";

		// The header for the player name column in the high score list.
		public const string PlayerHeader = "Player";

		// The header for the number of games played in the high score list.
		public const string GamesHeader = "Games";

		// The header for the average guesses column in the high score list.
		public const string AverageGuessesHeader = "Avg. Guesses";

		// The message displayed when the user has read the game instructions.
		public const string InstructionsReadMessage = "You’ve read the game instructions. Press any key to start playing...";

		// The message prompting the user to press any key to start playing.
		public const string StartPlayingPrompt = "Press any key to start playing...";

		// The message displayed after finishing the game, prompting to see the high score.
		public const string FinishGamePrompt = "\nCongratulations on finishing the game! Press any key to see your high score...";

		// The message prompting the user to press any key to continue.
		public const string ContinuePrompt = "\nPress any key to continue...";
	}
}