using Laboration.GameLogic.Interfaces;
using Laboration.UI.Interfaces;

namespace Laboration.GameFlow.Interfaces
{
	public interface IGameFlowController
	{
		void ExecuteGameLoop(IUserInterface userInterface, IGameLogic gameLogic);
	}
}