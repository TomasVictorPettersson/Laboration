using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Interfaces;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Implementations
{
	// Implementation of the IGameFactory interface for
	// creating game-related components for the MasterMind game.
	public class MasterMindGameFactory : IGameFactory
	{
		// Creates an instance of IDependencyInitializer to set up game dependencies.
		public IDependencyInitializer CreateDependencyInitializer()
		{
			return new GameDependencyInitializer(GameTypes.MasterMind);
		}

		// Creates an instance of IGameFlowController to manage the game flow.
		public IGameFlowController CreateGameFlowController()
		{
			return new GameFlowController(GameTypes.MasterMind);
		}
	}
}