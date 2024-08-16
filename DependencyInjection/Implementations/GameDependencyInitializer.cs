using GameLogic.Implementations;
using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameLogic.Implementations;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Implementations;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Implementations;
using Laboration.Validation.Interfaces;

namespace Laboration.DependencyInjection.Implementations
{
	// Initializes dependencies for the specified game type.
	public class GameDependencyInitializer(GameTypes gameType) : IDependencyInitializer
	{
		// Creates and returns instances of dependencies used in the game loop based on the game type.
		public (IConsoleUI consoleUI, IGameLogic gameLogic) InitializeDependencies()
		{
			// Initialize common dependencies
			IValidation validation = new GameValidation();
			IHighScoreManager highScoreManager = new HighScoreManager(gameType);
			IConsoleUI consoleUI = new GameConsoleUI(validation, highScoreManager);
			IGameLogic gameLogic = gameType switch
			{
				GameTypes.BullsAndCows => new BullsAndCowsGameLogic(highScoreManager, consoleUI, validation),
				GameTypes.MasterMind => new MasterMindGameLogic(highScoreManager, consoleUI, validation),
				_ => throw new ArgumentException("Invalid game type", nameof(gameType)),
			};
			return (consoleUI, gameLogic);
		}
	}
}