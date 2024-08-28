using GameResources.Enums;

namespace GameFlow.Implementations
{
	// Concrete implementation for Bulls and Cows game.
	public class BullsAndCowsGameFlowController : GameFlowControllerBase
	{
		public BullsAndCowsGameFlowController() : base(GameTypes.BullsAndCows)
		{
		}
	}
}