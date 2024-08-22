using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;

namespace Laboration.HighScoreManagement.Implementations
{
	// Concrete implementation of HighScoreManagerBase for managing high scores in the MasterMind game.
	// Specifies the file path for saving and loading high scores specific to the MasterMind game.
	public class MasterMindHighScoreManager : HighScoreManagerBase
	{
		// Constructor initializes the base class with the game type MasterMind.
		public MasterMindHighScoreManager() : base(GameTypes.MasterMind)
		{
		}

		// Provides the file path for storing high scores specific to the MasterMind game.
		public override string GetFilePath()
		{
			return FileConstants.MasterMindFilePath;
		}
	}
}