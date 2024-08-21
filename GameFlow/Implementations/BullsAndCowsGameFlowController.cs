using Laboration.GameResources.Enums;

namespace Laboration.GameFlow.Implementations
{
	// Concrete implementation for Bulls and Cows game.
	public class BullsAndCowsGameFlowController : GameFlowControllerBase
	{
		public BullsAndCowsGameFlowController() : base(GameTypes.BullsAndCows)
		{
		}
	}
}