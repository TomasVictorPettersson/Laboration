using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFlow.Interfaces;

namespace Laboration.GameFactory.Interfaces
{
	// Defines a factory interface for creating game-related components.
	public interface IGameFactory
	{
		// Creates an instance of IDependencyInitializer to set up game dependencies.
		IDependencyInitializer CreateDependencyInitializer();

		// Creates an instance of IGameFlowController to manage the game flow.
		IGameFlowController CreateGameFlowController();
	}
}