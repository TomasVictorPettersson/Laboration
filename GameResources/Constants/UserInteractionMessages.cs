namespace GameResources.Constants
{
	// Contains messages related to user interaction, including validation and options.
	public static class UserInteractionMessages
	{
		// Game Selection
		public const string GameSelectionOptions = "1. Bulls and Cows\n2. MasterMind";

		public const string ExitOption = "3. Exit";
		public const string GameSelectionInvalidMessage = "Invalid selection. Please enter a valid number.";

		// Username
		public const string EmptyUsernameMessage = "Empty values are not allowed. Please enter a valid username.\n";

		public const string UsernameLengthMessage = "Username must be between 2 and 20 characters long.\n";

		// Guess Input
		public const string BullsAndCowsInvalidInputMessage = "Invalid input. Please enter a 4-digit number with unique digits.\n";

		public const string MasterMindInvalidInputMessage = "Invalid input. Please enter a 4-digit number.\n";

		// Play Again
		public const string InvalidPlayAgainResponse = "Invalid input. Please enter y for yes or n for no.\n";
	}
}