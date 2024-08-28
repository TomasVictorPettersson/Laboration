using GameResources.Constants;
using GameResources.Enums;

namespace HighScoreManagement.Implementations
{
	// Concrete implementation of HighScoreManagerBase for managing high scores in the MasterMind game.
	// Specifies the file path for saving and loading high scores specific to the MasterMind game.
	public class MasterMindHighScoreManager : HighScoreManagerBase
	{
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