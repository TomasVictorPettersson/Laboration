namespace Laboration.GameResources.Constants
{
	// Contains static messages for the games.
	public static class GameMessages
	{
		// Welcome message for Bulls and Cows with gameplay instructions and feedback format.
		public const string BullsAndCowsWelcomeMessageFormat =
			"Welcome, {0}, to Bulls and Cows!\n\n" +
			"Guess a 4-digit number where each digit is unique (0-9).\n\n" +
			"Feedback format: ‘BBBB,CCCC’, where:\n" +
			"- ‘BBBB’ is the number of correct digits in the correct positions.\n" +
			"- ‘CCCC’ is the number of correct digits in the wrong positions.\n\n" +
			"If 'No matches found', none of your guessed digits are in the 4-digit number.";

		// Goodbye message for Bulls and Cows.
		public const string BullsAndCowsGoodbyeMessageFormat = "Thank you, {0}, for playing Bulls and Cows!";

		// Welcome message for MasterMind with gameplay instructions and feedback format.
		public const string MasterMindWelcomeMessageFormat =
			"Welcome, {0}, to MasterMind!\n\n" +
			"Guess a 4-digit number where digits can repeat (0-9).\n\n" +
			"Feedback format: ‘BBBB,CCCC’, where:\n" +
			"- ‘BBBB’ is the number of correct digits in the correct positions.\n" +
			"- ‘CCCC’ is the number of correct digits in the wrong positions.\n\n" +
			"If 'No matches found', none of your guessed digits are in the 4-digit number.";

		// Goodbye message for MasterMind.
		public const string MasterMindGoodbyeMessageFormat = "Thank you, {0}, for playing MasterMind!";

		// Message shown when the player makes a correct guess, including the number of guesses.
		public const string CorrectGuessMessageFormat =
			"Correct! The 4-digit number was: {0}\nIt took you {1} {2}.";

		// Message for practice mode showing the secret number.
		public const string SecretNumberPracticeMessage = "For practice mode, the 4-digit number is: {0}";

		// Prefix for feedback messages.
		public const string FeedbackPrefix = "Feedback: ";

		// Message indicating no matches between guess and secret number.
		public const string NoMatchesFoundMessage = "No matches found";

		// Welcome back message for returning players.
		public const string WelcomeBackMessageFormat =
			"Welcome back, {0}!\nGlad to see you again. Good luck with your next game!";
	}
}