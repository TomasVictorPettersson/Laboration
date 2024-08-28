namespace GameResources.Constants
{
	// Contains constant values used for file operations for "Bulls and Cows" and "MasterMind".
	// These constants include file paths for saving high scores and delimiters for data serialization.

	public static class FileConstants
	{
		// The paths to the files where high scores for "Bulls and Cows" and "MasterMind" are saved.
		public const string BullsAndCowsFilePath = "bullsandcows_highscores.txt";

		public const string MasterMindFilePath = "mastermind_highscores.txt";

		// The separator used to delimit data fields in the high score file.
		public const string Separator = "#&#";
	}
}