using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;

namespace Laboration.HighScoreManagement.Implementations
{
	// Concrete implementation of HighScoreManagerBase for managing high scores in the Bulls and Cows game.
	// Specifies the file path for saving and loading high scores specific to the Bulls and Cows game.
	public class BullsAndCowsHighScoreManager : HighScoreManagerBase
	{
		// Constructor initializes the base class with the game type BullsAndCows.
		public BullsAndCowsHighScoreManager() : base(GameTypes.BullsAndCows)
		{
		}

		// Provides the file path for storing high scores specific to the Bulls and Cows game.
		protected override string GetFilePath()
		{
			return FileConstants.BullsAndCowsFilePath;
		}
	}
}