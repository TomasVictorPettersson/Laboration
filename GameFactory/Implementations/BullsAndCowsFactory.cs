using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Interfaces;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Implementations
{
	// Concrete factory for creating Bulls and Cows game-specific components.
	public class BullsAndCowsFactory : GameFactoryBase
	{
		public BullsAndCowsFactory() : base(GameTypes.BullsAndCows)
		{
		}

		// This method creates and returns an instance of the BullsAndCowsDependencyInitializer,
		public override IDependencyInitializer CreateDependencyInitializer()
		{
			return new BullsAndCowsDependencyInitializer();
		}

		// This method creates and returns an instance of the BullsAndCowsGameFlowController.
		public override IGameFlowController CreateGameFlowController()
		{
			return new BullsAndCowsGameFlowController();
		}
	}
}