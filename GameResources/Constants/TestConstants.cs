namespace Laboration.GameResources.Constants
{
	// Contains constants used for testing purposes in the game.
	// These constants are designed to provide predefined values for various test scenarios.
	public static class TestConstants
	{
		// Usernames for various test scenarios.
		public const string UserName = "TestUser";

		public const string UserNameJohnDoe = "JohnDoe";
		public const string UserNameJaneDoe = "JaneDoe";

		// Testing values for gameplay and statistics.
		public const string SecretNumber = "1234"; // Example secret number for tests

		public const string Guess = "1234"; // Example guess value for tests
		public const string TotalGamesPlayed = "10"; // Example total games played value
		public const string AverageGuesses = "5,50"; // Example average guesses value
		public const int NumberOfGuesses = 10; // Example number of guesses for tests
		public const int SingleGuess = 1; // Example single guess value
		public const int MultipleGuesses = 5; // Example multiple guesses value

		// Testing limits and formatting.
		public const int MaxUserNameLength = 20; // Maximum length of a username for testing

		public const string Rank = "1"; // Example rank value
		public const int TotalWidth = 50; // Width used for formatting tables or UI elements

		// Feedback strings used for testing scenarios.
		public const string FeedbackBBBB = "BBBB,"; // Example feedback format for 'BBBB'

		public const string FeedbackComma = ","; // Example feedback comma
		public const string FeedbackCCCC = ",CCCC"; // Example feedback format for 'CCCC'
		public const string FeedbackBBCC = "BB,CC"; // Example feedback format for 'BBCC'
		public const string FeedbackBBComma = "BB,"; // Example feedback format with 'BB,'
		public const string FeedbackCommaCC = ",CC"; // Example feedback format with ',CC'

		// The file name used for storing high scores during unit tests.

		public const string HighScoresFileName = "test_highscores.txt";
	}
}