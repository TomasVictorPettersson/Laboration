using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Interfaces;

namespace Laboration.GameFlow.Interfaces
{
	public interface IGameFlowController
	{
		void ExecuteGameLoop(IConsoleUI consoleUI, IGameLogic gameLogic);
	}
}