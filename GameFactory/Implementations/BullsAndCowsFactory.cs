using DependencyInjection.Implementations;
using DependencyInjection.Interfaces;
using GameResources.Enums;

namespace GameFactory.Implementations
{
	// Concrete factory for creating Bulls and Cows game-specific components.
	public class BullsAndCowsFactory : GameFactoryBase
	{
		public BullsAndCowsFactory() : base(GameTypes.BullsAndCows)
		{
		}

		// This method creates and returns an instance of the BullsAndCowsDependencyInitializer,
		public override IDependencyInitializer CreateDependencyInitializer()
		{
			return new BullsAndCowsDependencyInitializer();
		}
	}
}