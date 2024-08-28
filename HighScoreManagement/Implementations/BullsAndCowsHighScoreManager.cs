using GameResources.Constants;
using GameResources.Enums;

namespace HighScoreManagement.Implementations
{
	// Concrete implementation of HighScoreManagerBase for managing high scores in the Bulls and Cows game.
	// Specifies the file path for saving and loading high scores specific to the Bulls and Cows game.
	public class BullsAndCowsHighScoreManager : HighScoreManagerBase
	{
		public BullsAndCowsHighScoreManager() : base(GameTypes.BullsAndCows)
		{
		}

		// Provides the file path for storing high scores specific to the Bulls and Cows game.
		public override string GetFilePath()
		{
			return FileConstants.BullsAndCowsFilePath;
		}
	}
}