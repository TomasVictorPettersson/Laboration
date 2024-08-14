namespace Laboration.GameResources.Constants
{
	// Contains user interaction related constants.
	public static class UserInteractionMessages
	{
		public const string UserNamePrompt = "Enter your username: ";
		public const string EmptyUsernameMessage = "Empty values are not allowed. Please enter a valid username.\n";
		public const string UsernameLengthMessage = "Username must be between 2 and 20 characters long.\n";
		public const string GuessPrompt = "Enter your 4-digit guess: ";
		public const string InvalidInputMessage = "Invalid input. Please enter a 4-digit number with unique digits.\n";
		public const string PlayAgainPrompt = "Do you want to play again? (y/n): ";
		public const string InvalidPlayAgainResponse = "Invalid input. Please enter y for yes or n for no.\n";
	}
}