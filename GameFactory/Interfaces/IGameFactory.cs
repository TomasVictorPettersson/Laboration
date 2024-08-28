using DependencyInjection.Interfaces;
using GameFlow.Interfaces;

namespace GameFactory.Interfaces
{
	// Defines the contract for a factory that creates game-related components.
	public interface IGameFactory
	{
		// Creates and returns an instance of IDependencyInitializer for setting up game dependencies.
		IDependencyInitializer CreateDependencyInitializer();

		// Creates and returns an instance of IGameFlowController for managing the game flow.
		IGameFlowController CreateGameFlowController();
	}
}