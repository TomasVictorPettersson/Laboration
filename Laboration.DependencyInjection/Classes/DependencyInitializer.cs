using Laboration.Configurations.Classes;
using Laboration.DataManagement.Classes;
using Laboration.DataManagement.Interfaces;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameLogic.Classes;
using Laboration.GameLogic.Interfaces;
using Laboration.UI.Classes;
using Laboration.UI.Interfaces;

namespace Laboration.DependencyInjection.Classes
{
	// Handles the initialization of dependencies.
	public class DependencyInitializer : IDependencyInitializer
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