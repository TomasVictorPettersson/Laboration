using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Implementations;
using Laboration.Validation.Interfaces;

namespace Laboration.DependencyInjection.Implementations
{
	// Abstract base class for initializing game dependencies.
	public abstract class GameDependencyInitializerBase : IDependencyInitializer
	{
		protected readonly GameTypes GameType;

		protected GameDependencyInitializerBase(GameTypes gameType)
		{
			GameType = gameType;
		}

		// Abstract method to create and return specific game logic.
		protected abstract IGameLogic CreateGameLogic(IConsoleUI consoleUI, IValidation validation, IHighScoreManager highScoreManager);

		// Creates and returns instances of dependencies used in the game loop.
		public (IConsoleUI consoleUI, IGameLogic gameLogic) InitializeDependencies()
		{
			// Initialize common dependencies
			IValidation validation = new ValidationBase();
			IHighScoreManager highScoreManager = new HighScoreManager(GameType);
			IConsoleUI consoleUI = new ConsoleUIBase(validation, highScoreManager);
			IGameLogic gameLogic = CreateGameLogic(consoleUI, validation, highScoreManager);

			return (consoleUI, gameLogic);
		}
	}
}