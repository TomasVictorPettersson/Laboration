using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameLogic.Implementations;
using Laboration.GameLogic.Interfaces;
using Laboration.HighScoreManagement.Implementations;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Implementations;
using Laboration.Validation.Interfaces;

namespace Laboration.DependencyInjection.Implementations
{
	// Initializes dependencies for the Bulls and Cows game.
	public class GameDependencyInitializer : IDependencyInitializer
	{
		// Creates and returns instances of dependencies used in the game loop.
		public (IConsoleUI consoleUI, IGameLogic gameLogic) InitializeDependencies()
		{
			// Initialize dependencies
			IValidation validation = new GameValidation();
			IHighScoreManager highScoreManager = new HighScoreManager();
			IConsoleUI consoleUI = new GameConsoleUI(validation, highScoreManager);
			IGameLogic gameLogic = new BullsAndCowsGameLogic(highScoreManager, consoleUI, validation);
			return (consoleUI, gameLogic);
		}
	}
}