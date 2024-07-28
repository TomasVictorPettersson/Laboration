using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameLogic.Implementations;
using Laboration.GameLogic.Interfaces;
using Laboration.HighScoreManagement.Implementations;
using Laboration.HighScoreManagement.Interfaces;

namespace Laboration.DependencyInjection.Implementations
{
	// Initializes dependencies for the Bulls and Cows game.
	public class BullsAndCowsDependencyInitializer : IDependencyInitializer
	{
		// Creates and returns instances of dependencies used in the game loop.
		public (IConsoleUI consoleUI, IGameLogic gameLogic) InitializeDependencies()
		{
			IConsoleUI consoleUI = new BullsAndCowsConsoleUI();

			IHighScoreManager highScoreManager = new BullsAndCowsHighScoreManager();

			IGameLogic gameLogic = new BullsAndCowsGameLogic(highScoreManager, consoleUI);

			return (consoleUI, gameLogic);
		}
	}
}