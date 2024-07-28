using Laboration.Configurations;
using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameLogic.Implementations;
using Laboration.GameLogic.Interfaces;
using Laboration.HighScoreManagement.Implementations;
using Laboration.HighScoreManagement.Interfaces;

namespace Laboration.DependencyInjection.Implementations
{
	// Handles the initialization of dependencies specifically for the Bulls and Cows game.
	public class BullsAndCowsDependencyInitializer : IDependencyInitializer
	{
		// Initializes dependencies and returns them for use in the game loop.
		public (IConsoleUI consoleUI, IGameLogic gameLogic) InitializeDependencies()
		{
			IConsoleUI consoleUI = new BullsAndCowsConsoleUI();
			IHighScoreManager highScoreManager = new BullsAndCowsHighScoreManager();
			GameSettings gameSettings = new();
			IGameLogic gameLogic = new BullsAndCowsGameLogic(highScoreManager, consoleUI, gameSettings);

			return (consoleUI, gameLogic);
		}
	}
}