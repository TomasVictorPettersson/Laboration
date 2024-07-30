using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Interfaces;

namespace Laboration.GameFlow.Interfaces
{
	// Defines the contract for managing the game flow in a game application.
	public interface IGameFlowController
	{
		// Executes the main game loop, coordinating between the user interface and game logic.
		void ExecuteGameLoop(IConsoleUI consoleUI, IGameLogic gameLogic);
	}
}