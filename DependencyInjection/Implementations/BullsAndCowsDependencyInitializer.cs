using Laboration.DataManagement.Classes;
using Laboration.DataManagement.Interfaces;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameLogic.Classes;
using Laboration.GameLogic.Interfaces;

namespace Laboration.DependencyInjection.Implementations
{
	// Handles the initialization of dependencies specifically for the Bulls and Cows game.
	public class BullsAndCowsDependencyInitializer : IDependencyInitializer
	{
		// Initializes dependencies and returns them for use in the game loop.
		public (IUserInterface userInterface, IGameLogic gameLogic) InitializeDependencies()
		{
			IUserInterface userInterface = new UserInterface();
			IHighScoreManager highScoreManager = new HighScoreManager();
			GameConfig gameConfig = new();
			IGameLogic gameLogic = new BullsAndCowsGameLogic(highScoreManager, userInterface, gameConfig);

			return (userInterface, gameLogic);
		}
	}
}