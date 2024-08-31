using DependencyInjection.Interfaces;
using GameFactory.Interfaces;
using GameFlow.Implementations;
using GameFlow.Interfaces;
using GameResources.Enums;

namespace GameFactory.Implementations
{
	// Abstract base class for creating game-related components.
	public abstract class GameFactoryBase(GameTypes gameType) : IGameFactory
	{
		// Readonly property to store the game type.
		public readonly GameTypes GameType = gameType;

		// Abstract method to create specific game components.
		public abstract IDependencyInitializer CreateDependencyInitializer();

		// Method to create the game flow controller.
		public IGameFlowController CreateGameFlowController()
		{
			return new GameFlowController(GameType);
		}
	}
}