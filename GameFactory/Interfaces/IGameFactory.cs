using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFlow.Interfaces;

namespace Laboration.GameFactory.Interfaces
{
	public interface IGameFactory
	{
		IDependencyInitializer CreateDependencyInitializer();

		IGameFlowController CreateGameFlowController();
	}
}