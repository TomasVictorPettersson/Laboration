using GameFactory.Interfaces;
using GameResources.Enums;

namespace GameFactory.Implementations
{
	// Implements the IGameFactoryCreator interface to create game factory instances.
	public class GameFactoryCreator : IGameFactoryCreator
	{
		// Creates the appropriate game factory based on the selected game type.
		public IGameFactory? CreateGameFactory(GameTypes gameType)
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