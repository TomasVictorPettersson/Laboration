namespace Laboration.GameResources.Constants
{
	// Contains constants used for testing purposes in the game.
	public static class TestConstants
	{
		// Usernames for various test scenarios.
		public const string UserName = "TestUser";

		public const string UserNameJohnDoe = "JohnDoe";
		public const string UserNameJaneDoe = "JaneDoe";

		// Testing values for gameplay and statistics.
		public const string SecretNumber = "1234";

		public const string Guess = "1234";
		public const string TotalGamesPlayed = "10";
		public const string AverageGuesses = "5,50";
		public const int NumberOfGuesses = 10;
		public const int SingleGuess = 1;
		public const int MultipleGuesses = 5;

		// Testing limits and formatting.
		public const int MaxUserNameLength = 20;

		public const string Rank = "1";
		public const int TotalWidth = 50;  // Used for formatting tables or UI elements

		// Feedback strings used for testing scenarios, such as validating output formatting or parsing.
		public const string FeedbackBBBB = "BBBB,";

		public const string FeedbackComma = ",";
		public const string FeedbackCCCC = ",CCCC";
		public const string FeedbackBBCC = "BB,CC";
		public const string FeedbackBBComma = "BB,";
		public const string FeedbackCommaCC = ",CC";
	}
}