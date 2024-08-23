namespace Laboration.GameResources.Constants
{
	// Contains static messages used throughout the Bulls and Cows game.
	// These messages are formatted strings used for user interaction and feedback.
	public static class GameMessages
	{
		// Welcome message format for Bulls and Cows.
		// Includes instructions on how to play and format of feedback.

		public const string BullsAndCowsWelcomeMessageFormat =
			"Welcome, {0}, to Bulls and Cows!\n\n" +
			"The goal is to guess a 4-digit number where each digit is unique and between 0 and 9.\n\n" +
			"For each guess, you’ll get feedback in the format ‘BBBB,CCCC’, where:\n" +
			"- ‘BBBB’ is the number of bulls (correct digits in the correct positions).\n" +
			"- ‘CCCC’ is the number of cows (correct digits in the wrong positions).\n\n" +
			"If you get 'No matches found', none of your guessed digits are in the 4-digit number.";

		// Goodbye message format for Bulls and Cows.
		// Thanks the user for playing and provides a closing statement.

		public const string BullsAndCowsGoodbyeMessageFormat = "Thank you, {0}, for playing Bulls and Cows!";

		// Welcome message format for MasterMind.
		// Includes instructions on how to play and format of feedback.

		public const string MasterMindWelcomeMessageFormat =
			"Welcome, {0}, to MasterMind!\n\n" +
			"The goal is to guess a 4-digit number where each digit can be between 0 and 9 and may appear more than once.\n\n" +
			"For each guess, you’ll get feedback in the format ‘BBBB,CCCC’, where:\n" +
			"- ‘BBBB’ is the number of digits in the correct positions.\n" +
			"- ‘CCCC’ is the number of digits that are correct but in the wrong positions.\n\n" +
			"If you get 'No matches found', none of your guessed digits are in the 4-digit number.";

		// Goodbye message format for MasterMind.
		// Thanks the user for playing and provides a closing statement.

		public const string MasterMindGoodbyeMessageFormat = "Thank you, {0}, for playing MasterMind!";

		// Message displayed when the player makes a correct guess.
		// Shows the secret number and the number of guesses taken to get it right.
		public const string CorrectGuessMessageFormat =
			"Correct! The 4-digit number was: {0}\nIt took you {1} {2}.";

		// Message displayed in practice mode to show the secret number.
		// Helps users practice by revealing the number they are trying to guess.
		public const string SecretNumberPracticeMessage = "For practice mode, the 4-digit number is: {0}";

		// Prefix used for feedback messages to the user.
		// Helps differentiate feedback from other types of messages.
		public const string FeedbackPrefix = "Feedback: ";

		// Message indicating that no digits from the guess match the secret number.
		// Informs the user that their guess was completely incorrect.
		public const string NoMatchesFoundMessage = "No matches found";

		// Message displayed to the user when they return for another game.
		// Offers a warm welcome back and good luck for the next game.
		public const string WelcomeBackMessageFormat =
			"Welcome back, {0}!\nGlad to see you again. Good luck with your next game!";
	}
}