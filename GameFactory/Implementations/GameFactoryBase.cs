using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFlow.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Interfaces
{
	// Abstract base class for creating game-related components.
	public abstract class GameFactoryBase(GameTypes gameType) : IGameFactory
	{
		protected readonly GameTypes GameType = gameType;

		// Abstract methods to create specific components.
		public abstract IDependencyInitializer CreateDependencyInitializer();

		public abstract IGameFlowController CreateGameFlowController();

		// Factory methods to get game components.
		public IDependencyInitializer GetDependencyInitializer() => CreateDependencyInitializer();

		public IGameFlowController GetGameFlowController() => CreateGameFlowController();
	}
}