namespace Laboration.Configurations.Classes
{
	// Represents configuration settings for the game.
	public class GameConfig
	{
		// Maximum number of retries allowed; default is no limit.
		public int MaxRetries { get; set; } = int.MaxValue;
	}
}