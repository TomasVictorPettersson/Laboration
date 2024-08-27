using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Interfaces;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Implementations
{
	// Concrete factory for creating MasterMind game-specific components.
	public class MasterMindFactory : GameFactoryBase
	{
		public MasterMindFactory() : base(GameTypes.MasterMind)
		{
		}

		// This method creates and returns an instance of the MasterMindDependencyInitializer,
		public override IDependencyInitializer CreateDependencyInitializer()
		{
			return new MasterMindDependencyInitializer();
		}

		// This method creates and returns an instance of the MasterMindGameFlowController.
		public override IGameFlowController CreateGameFlowController()
		{
			return new MasterMindGameFlowController();
		}
	}
}