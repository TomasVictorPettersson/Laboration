using Laboration.ConsoleUI.Interfaces;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;

namespace Laboration.DependencyInjection.Implementations
{
	// Abstract base class for initializing game dependencies.
	public abstract class GameDependencyInitializerBase(GameTypes gameType) : IDependencyInitializer
	{
		protected readonly GameTypes GameType = gameType;

		// Abstract method to create and return specific game logic.
		protected abstract IGameLogic CreateGameLogic(IConsoleUI consoleUI, IValidation validation, IHighScoreManager highScoreManager);

		// Abstract method to create and return the specific validation implementation.
		protected abstract IValidation CreateValidation();

		// Abstract method to create and return the specific console UI implementation.
		protected abstract IConsoleUI CreateConsoleUI(IValidation validation, IHighScoreManager highScoreManager);

		// Abstract method to create and return the specific high score manager implementation.
		protected abstract IHighScoreManager CreateHighScoreManager();

		// Creates and returns instances of dependencies used in the game loop.
		public (IConsoleUI consoleUI, IGameLogic gameLogic) InitializeDependencies()
		{
			// Initialize specific dependencies
			IValidation validation = CreateValidation();
			IHighScoreManager highScoreManager = CreateHighScoreManager();
			IConsoleUI consoleUI = CreateConsoleUI(validation, highScoreManager);
			IGameLogic gameLogic = CreateGameLogic(consoleUI, validation, highScoreManager);

			return (consoleUI, gameLogic);
		}
	}
}