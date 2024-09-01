using ConsoleUI.Implementations;
using ConsoleUI.Interfaces;
using GameFactory.Implementations;
using GameFactory.Interfaces;
using Validation.Implementations;
using GameApplication.Interfaces;

namespace GameApplication.Implementations
{
	// Configures and provides instances of game selection and factory creator.
	public class DependencyConfigurator : IDependencyConfigurator
	{
		// Initializes and returns instances of:
		// - IGameSelection: Manages game type selection.
		// - IGameFactoryCreator: Creates game factories.
		public (IGameSelection, IGameFactoryCreator) ConfigureDependencies()
		{
			var gameSelectionValidation = new GameSelectionValidation();
			var gameSelection = new GameSelection(gameSelectionValidation);
			var factoryCreator = new GameFactoryCreator();

			return (gameSelection, factoryCreator);
		}
	}
}