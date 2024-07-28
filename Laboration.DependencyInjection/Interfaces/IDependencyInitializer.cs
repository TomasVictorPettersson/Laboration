using Laboration.GameLogic.Interfaces;
using Laboration.UI.Interfaces;

namespace Laboration.DependencyInjection.Interfaces
{
	public interface IDependencyInitializer
	{
		(IUserInterface userInterface, IGameLogic gameLogic) InitializeDependencies();
	}
}