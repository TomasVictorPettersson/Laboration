using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Interfaces;

namespace Laboration.DependencyInjection.Interfaces
{
	public interface IDependencyInitializer
	{
		(IConsoleUI consoleUI, IGameLogic gameLogic) InitializeDependencies();
	}
}