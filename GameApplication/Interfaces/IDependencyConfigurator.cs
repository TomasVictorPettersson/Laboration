using ConsoleUI.Interfaces;
using GameFactory.Interfaces;

namespace GameApplication.Interfaces
{
	// Interface for configuring game application dependencies.
	public interface IDependencyConfigurator
	{
		// Returns:
		// - IGameSelection: Handles game selection.
		// - IGameFactoryCreator: Creates game factories.
		(IGameSelection, IGameFactoryCreator) ConfigureDependencies();
	}
}