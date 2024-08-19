using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Interfaces;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Implementations
{
	// Generic implementation of IGameFactory for creating game-related components.
	public class GameComponentFactory(GameTypes gameType) : IGameFactory
	{
		private readonly GameTypes _gameType = gameType;

		// Creates an instance of IDependencyInitializer to set up game dependencies.
		public IDependencyInitializer CreateDependencyInitializer()
		{
			return new GameDependencyInitializer(_gameType);
		}

		// Creates an instance of IGameFlowController to manage the game flow.
		public IGameFlowController CreateGameFlowController()
		{
			return new GameFlowController(_gameType);
		}
	}
}