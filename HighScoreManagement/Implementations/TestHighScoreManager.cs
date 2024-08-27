using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;

namespace Laboration.HighScoreManagement.Implementations
{
	// Implementation of a high score manager for testing purposes.
	public class TestHighScoreManager : HighScoreManagerBase
	{
		public TestHighScoreManager() : base(GameTypes.Test)
		{
		}

		// Returns the file path for storing high scores specific to testing.
		public override string GetFilePath()
		{
			// Combines the system's temporary path with the test-specific file path constant.
			return Path.Combine(Path.GetTempPath(), TestConstants.TestFilePath);
		}
	}
}