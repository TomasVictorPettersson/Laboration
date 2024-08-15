namespace Laboration.GameResources.Constants
{
	// Contains constants used for formatting and presentation in the Bulls and Cows game.
	// These constants define column widths and padding for displaying data in a formatted manner.
	public static class FormattingConstants
	{
		// Width of the column for displaying the player's rank.
		// Ensures that the rank is aligned properly in the output.
		public const int RankColumnWidth = 6;

		// Width of the column for displaying the total number of games played by a player.
		// Provides enough space to accommodate numbers and align with other columns.
		public const int GamesPlayedColumnWidth = 8;

		// Width of the column for displaying the average number of guesses per game.
		// Allows for proper alignment of average values in the output.
		public const int AverageGuessesColumnWidth = 15;

		// Number of spaces used for padding between columns in the formatted output.
		// Improves readability and separation of columns in the display.
		public const int Padding = 3;
	}
}