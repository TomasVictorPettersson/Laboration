using DependencyInjection.Interfaces;
using GameFlow.Interfaces;
using GameResources.Enums;

namespace GameFactory.Interfaces
{
	// Abstract base class for creating game-related components.
	public abstract class GameFactoryBase(GameTypes gameType) : IGameFactory
	{
		// Readonly property to store the game type.
		public readonly GameTypes GameType = gameType;

		// Abstract methods to create specific components.
		public abstract IDependencyInitializer CreateDependencyInitializer();

		public abstract IGameFlowController CreateGameFlowController();
	}
}