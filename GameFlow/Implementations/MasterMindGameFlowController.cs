using GameResources.Enums;

namespace GameFlow.Implementations
{
	// Concrete implementation for MasterMind game.
	public class MasterMindGameFlowController : GameFlowControllerBase
	{
		public MasterMindGameFlowController() : base(GameTypes.MasterMind)
		{
		}
	}
}