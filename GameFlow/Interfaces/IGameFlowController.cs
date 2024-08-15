using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Interfaces;

namespace Laboration.GameFlow.Interfaces
{
	// Defines the contract for controlling the game flow in a game application.
	public interface IGameFlowController
	{
		// Runs the main game loop, coordinating interactions between the user interface and game logic.
		void ExecuteGameLoop(IConsoleUI consoleUI, IGameLogic gameLogic);
	}
}