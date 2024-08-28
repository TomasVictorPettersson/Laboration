using ConsoleUI.Interfaces;
using GameLogic.Interfaces;
using HighScoreManagement.Interfaces;
using Validation.Interfaces;

namespace DependencyInjection.Interfaces
{
	// Defines the contract for initializing game dependencies.
	public interface IDependencyInitializer
	{
		// Initializes and returns the required game dependencies.
		// Returns a tuple with instances of IConsoleUI and IGameLogic.
		(IConsoleUI consoleUI, IGameLogic gameLogic) InitializeDependencies();

		// Abstract method to create and return specific game logic.
		// To be implemented by classes to provide game-specific logic.
		IGameLogic CreateGameLogic(IConsoleUI consoleUI, IValidation validation, IHighScoreManager highScoreManager);

		// Abstract method to create and return the specific validation implementation.
		// To be implemented by classes to provide game-specific validation.
		IValidation CreateValidation();

		// Abstract method to create and return the specific console UI implementation.
		// To be implemented by classes to provide game-specific console UI.
		IConsoleUI CreateConsoleUI(IValidation validation, IHighScoreManager highScoreManager);

		// Abstract method to create and return the specific high score manager implementation.
		// To be implemented by classes to provide game-specific high score management.
		IHighScoreManager CreateHighScoreManager();
	}
}