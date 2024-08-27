namespace Laboration.GameResources.Constants
{
	// Contains static messages for the games.
	public static class GameMessages
	{
		// Welcome message for Bulls and Cows with gameplay instructions.
		public const string BullsAndCowsWelcomeMessageFormat =
			"Welcome, {0}, to Bulls and Cows!\n\n" +
			"The goal is to guess a 4-digit number where each digit is unique and between 0 and 9.\n\n" +
			FeedbackInstructions;

		// Goodbye message for Bulls and Cows.
		public const string BullsAndCowsGoodbyeMessageFormat = "Thank you, {0}, for playing Bulls and Cows!";

		// Welcome message for MasterMind with gameplay instructions.
		public const string MasterMindWelcomeMessageFormat =
		"Welcome, {0}, to MasterMind!\n\n" +
		"The goal is to guess a 4-digit number where digits can repeat and range from 0 to 9.\n\n" +
		FeedbackInstructions;

		// Goodbye message for MasterMind.
		public const string MasterMindGoodbyeMessageFormat = "Thank you, {0}, for playing MasterMind!";

		// Feedback instructions for both games.
		public const string FeedbackInstructions =
			"For each guess, you’ll get feedback in the format ‘BBBB,CCCC’, where:\n" +
			"- ‘BBBB’ is the number of bulls (correct digits in the correct positions).\n" +
			"- ‘CCCC’ is the number of cows (correct digits in the wrong positions).\n\n" +
			"If you get 'No matches found', none of your guessed digits are in the 4-digit number.";

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