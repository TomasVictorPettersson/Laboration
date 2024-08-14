namespace Laboration.GameResources.Constants
{
	// Contains game-related messages.
	public static class GameMessages
	{
		public const string WelcomeMessageFormat =
			"Welcome, {0}, to Bulls and Cows!\n\n" +
			"The goal is to guess a 4-digit number where each digit is unique and between 0 and 9.\n\n" +
			"For each guess, you’ll get feedback in the format ‘BBBB,CCCC’, where:\n" +
			"- ‘BBBB’ is the number of bulls (correct digits in the correct positions).\n" +
			"- ‘CCCC’ is the number of cows (correct digits in the wrong positions).\n\n" +
			"If you get 'No matches found', none of your guessed digits are in the 4-digit number.";

		public const string WelcomeBackMessageFormat =
			"Welcome back, {0}!\nGlad to see you again. Good luck with your next game!";

		public const string CorrectGuessMessageFormat =
			"Correct! The 4-digit number was: {0}\nIt took you {1} {2}.";

		public const string SecretNumberPracticeMessage = "For practice mode, the 4-digit number is: {0}";

		public const string FeedbackPrefix = "Feedback: ";
		public const string NoMatchesFoundMessage = "No matches found";

		public const string GoodbyeMessageFormat = "Thank you, {0}, for playing Bulls and Cows!";
	}
}