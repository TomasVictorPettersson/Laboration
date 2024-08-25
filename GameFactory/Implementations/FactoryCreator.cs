using Laboration.GameFactory.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Implementations
{
	// Implements the IFactoryCreator interface to create game factory instances.
	public class FactoryCreator : IFactoryCreator
	{
		// Creates the appropriate factory based on the selected game type.
		public IGameFactory? CreateFactory(GameTypes gameType)
		{
			return gameType switch
			{
				GameTypes.BullsAndCows => new BullsAndCowsFactory(),
				GameTypes.MasterMind => new MasterMindFactory(),
				GameTypes.Quit => null, // Explicitly handle the Quit game type by returning null
				_ => throw new ArgumentException("Invalid game type", nameof(gameType)),
			};
		}
	}
}