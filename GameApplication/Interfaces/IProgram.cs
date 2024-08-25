using Laboration.ConsoleUI.Interfaces;
using Laboration.GameFactory.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameApplication.Interfaces
{
	// Interface defining the contract for a game application program.
	public interface IProgram
	{
		// Runs the main game loop, allowing users to select game types and manage game execution.
		void RunGameLoop();

		// Initializes the game factory based on the provided game type.
		// Returns true if the factory was successfully created; otherwise, returns false.
		bool InitializeGameFactory(GameTypes gameType);

		// Initializes and returns the user interface and game logic dependencies.
		// Returns a tuple containing instances of IConsoleUI and IGameLogic.
		(IConsoleUI, IGameLogic) InitializeDependencies();

		// Provides access to the current game factory instance.
		// Returns the IGameFactory instance or null if not initialized.
		IGameFactory? GetFactory();
	}
}