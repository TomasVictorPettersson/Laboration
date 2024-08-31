using DependencyInjection.Implementations;
using DependencyInjection.Interfaces;
using GameResources.Enums;

namespace GameFactory.Implementations
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
	}
}