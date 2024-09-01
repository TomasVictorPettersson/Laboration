namespace GameResources.Constants
{
	// Contains constants used for testing purposes.
	// Provides predefined values for various test scenarios.
	public static class TestConstants
	{
		// Usernames used in test scenarios.
		public const string UserName = "TestUser";

		public const string UserNameJohnDoe = "JohnDoe";
		public const string UserNameJaneDoe = "JaneDoe";

		// Example values for gameplay and statistics testing.
		public const string SecretNumber = "1234"; // Example secret number

		public const string Guess = "1234"; // Example guess value
		public const string TotalGamesPlayed = "10"; // Example total games played
		public const string AverageGuesses = "5,50"; // Example average guesses value
		public const int NumberOfGuesses = 10; // Example number of guesses
		public const int SingleGuess = 1; // Example single guess
		public const int MultipleGuesses = 5; // Example multiple guesses

		// Limits and formatting values for testing.
		public const int MaxUserNameLength = 20; // Maximum username length

		public const string Rank = "1"; // Example rank value
		public const int TotalWidth = 50; // Width used for formatting tables or UI

		// Feedback formats used in testing scenarios.
		public const string FeedbackBBBB = "BBBB,"; // Feedback format for 'BBBB'

		public const string FeedbackCCCC = ",CCCC"; // Feedback format for 'CCCC'
		public const string FeedbackBBCC = "BB,CC"; // Feedback format for 'BB,CC'
		public const string FeedbackBBComma = "BB,"; // Feedback format with 'BB,'
		public const string FeedbackCommaCC = ",CC"; // Feedback format with ',CC'

		// File name used for storing high scores during unit tests.
		public const string TestFilePath = "test_highscores.txt";

		// Constants for input values used in tests
		public const string NullInput = null!;

		public const string EmptyInput = "";
		public const string NonNumericInput = "text";
		public const string InputNot4Digits = "123";
		public const string InputMoreThan4Digits = "12345";
		public const string RepeatingDigitsInput = "1122";
		public const string Valid4DigitUniqueNumber = "1234";
		public const string InputWithNonNumericCharacters = "12A4";
		public const string InputWithMixedCharacters = "1a34";
		public const string InputWithSpecialCharacters = "12@4";
		public const string InputWithLeadingOrTrailingSpaces = " 1234 ";
		public const string InputWithSpacesInBetween = "12 34";
		public const string InputWithAllZeroes = "0000";
		public const string InvalidInput = "invalid";
	}
}