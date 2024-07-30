using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Interfaces;

namespace Laboration.DependencyInjection.Interfaces
{
	// Defines a contract for initializing dependencies needed for the game.
	public interface IDependencyInitializer
	{
		// Initializes and returns the dependencies required for the game.
		// Returns a tuple containing instances of IConsoleUI and IGameLogic.
		(IConsoleUI consoleUI, IGameLogic gameLogic) InitializeDependencies();
	}
}