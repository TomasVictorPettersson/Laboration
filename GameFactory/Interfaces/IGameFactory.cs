using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFlow.Interfaces;

namespace Laboration.GameFactory.Interfaces
{
	// Defines the contract for a factory that creates game-related components.
	public interface IGameFactory
	{
		// Creates and returns an instance of IDependencyInitializer for setting up game dependencies.
		IDependencyInitializer CreateDependencyInitializer();

		// Creates and returns an instance of IGameFlowController for managing the game flow.
		IGameFlowController CreateGameFlowController();

		// Factory methods to get game components.
		IDependencyInitializer GetDependencyInitializer();

		IGameFlowController GetGameFlowController();
	}
}