using Laboration.GameResources.Enums;

namespace Laboration.HighScoreManagement.Implementations
{
	public class TestHighScoreManager : HighScoreManagerBase
	{
		public TestHighScoreManager() : base(GameTypes.Test) // Use a test-specific game type
		{
		}

		public override string GetFilePath()
		{
			return Path.Combine(Path.GetTempPath(), "test_highscores.txt");
		}
	}
}