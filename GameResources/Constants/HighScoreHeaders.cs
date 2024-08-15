namespace Laboration.GameResources.Constants
{
	// Contains constant values used as headers for high score lists in the Bulls and Cows game.
	// These headers are used to format and label columns in the high score table.
	public static class HighScoreHeaders
	{
		// Header for the main title of the high score list.
		// Displays at the top of the high score list to indicate the section's purpose.
		public const string HighScoreHeader = "=== HIGH SCORE LIST ===";

		// Header for the column displaying the player's rank in the high score list.
		// Used to show the ranking position of each player.
		public const string RankHeader = "Rank";

		// Header for the column displaying the player's username.
		// Used to identify which player achieved the score.
		public const string PlayerHeader = "Player";

		// Header for the column displaying the total number of games played by the player.
		// Shows the number of games a player has participated in.
		public const string GamesHeader = "Games";

		// Header for the column displaying the average number of guesses per game for the player.
		// Provides insight into the player's performance by showing their average guessing accuracy.
		public const string AverageGuessesHeader = "Avg. Guesses";
	}
}