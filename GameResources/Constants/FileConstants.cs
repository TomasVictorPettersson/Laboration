namespace Laboration.GameResources.Constants
{
	// Contains constant values used for file operations in the Bulls and Cows game.
	// These constants include file paths and delimiters used for data serialization.
	public static class FileConstants
	{
		// The path to the file where high scores are saved.
		// This file is used to persist high score data between game sessions.
		public const string FilePath = "bullsandcows_highscores.txt";

		// The separator used to delimit data fields in the high score file.
		// This delimiter separates username and number of guesses in each line of the file.
		public const string Separator = "#&#";
	}
}