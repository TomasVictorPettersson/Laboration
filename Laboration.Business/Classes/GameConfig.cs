namespace Laboration.Business.Classes
{
	// Represents configuration settings for the game, including maximum number of retries allowed.
	public class GameConfig
	{
		public int MaxRetries { get; set; } = int.MaxValue; // No limit for gameplay by default
	}
}