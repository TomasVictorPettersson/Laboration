using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Interfaces;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;

namespace Laboration.GameFactory.Implementations
{
	// Implementation of the IGameFactory interface for the Bulls and Cows game.
	public class BullsAndCowsGameFactory : IGameFactory
	{
		public IDependencyInitializer CreateDependencyInitializer()
		{
			return new BullsAndCowsDependencyInitializer();
		}

		public IGameFlowController CreateGameFlowController()
		{
			return new BullsAndCowsGameFlowController();
		}
	}
}