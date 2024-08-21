using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Interfaces;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Implementations
{
	// Concrete implementation for MasterMind game.
	public class MasterMindFactory : GameFactoryBase
	{
		public MasterMindFactory() : base(GameTypes.MasterMind)
		{
		}

		public override IDependencyInitializer CreateDependencyInitializer()
		{
			return new MasterMindDependencyInitializer();
		}

		public override IGameFlowController CreateGameFlowController()
		{
			return new MasterMindGameFlowController();
		}
	}
}