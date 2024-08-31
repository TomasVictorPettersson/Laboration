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
		// Read-only property to store the type of game that this factory will create components for.
		public GameTypes GameType { get; } = gameType;

		// Abstract method to create the dependency initializer for game components.
		// Derived classes must implement this method to provide a specific dependency initializer.
		public abstract IDependencyInitializer CreateDependencyInitializer();

		// Creates a new instance of GameFlowController using the stored game type.
		// Provides a default implementation for creating a game flow controller.
		public IGameFlowController CreateGameFlowController()
		{
			return new GameFlowController(GameType);
		}
	}
}