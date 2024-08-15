using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Interfaces;

namespace Laboration.DependencyInjection.Interfaces
{
	// Defines the contract for initializing game dependencies.
	public interface IDependencyInitializer
	{
		// Initializes and returns the required game dependencies.
		// Returns a tuple with instances of IConsoleUI and IGameLogic.
		(IConsoleUI ConsoleUI, IGameLogic GameLogic) InitializeDependencies();
	}
}