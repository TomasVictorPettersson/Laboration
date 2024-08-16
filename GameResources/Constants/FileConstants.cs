namespace Laboration.GameResources.Constants
{
	// Contains constant values used for file operations in different games.
	// These constants include file paths for saving high scores and delimiters for data serialization.

	public static class FileConstants
	{
		// The path to the file where high scores for the Bulls and Cows game are saved.
		// This file is used to persist high score data between game sessions.

		public const string BullsAndCowsFilePath = "bullsandcows_highscores.txt";

		// The path to the file where high scores for the MasterMind game are saved.
		// This file is used to persist high score data between game sessions.

		public const string MasterMindFilePath = "mastermind_highscores.txt";

		// The separator used to delimit data fields in the high score file.
		// This delimiter separates username and number of guesses in each line of the file.

		public const string Separator = "#&#";
	}
}