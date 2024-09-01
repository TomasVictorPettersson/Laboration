namespace GameResources.Constants
{
	// Contains constant values used throughout the game.
	// These constants include configuration settings such as the length of the secret number,
	// the range of digits, and characters used for feedback.
	public static class GameConstants
	{
		// Number of digits in the secret number
		public const int SecretNumberLength = 4;

		// Range for random digits (0-9)
		public const int DigitRange = 10;

		// Characters for feedback
		public const char BullCharacter = 'B';

		public const char CowCharacter = 'C';

		public const string FeedbackComma = ",";
	}
}