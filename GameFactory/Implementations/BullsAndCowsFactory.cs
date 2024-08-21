using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Interfaces;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Implementations
{
	// Concrete implementation for Bulls and Cows game.
	public class BullsAndCowsFactory : GameFactoryBase
	{
		public BullsAndCowsFactory() : base(GameTypes.BullsAndCows)
		{
		}

		public override IDependencyInitializer CreateDependencyInitializer()
		{
			return new BullsAndCowsDependencyInitializer();
		}

		public override IGameFlowController CreateGameFlowController()
		{
			return new BullsAndCowsGameFlowController();
		}
	}
}