using Laboration.GameFactory.Implementations;
using Laboration.GameFactory.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Creators
{
	// A static class responsible for creating game factory instances based on the selected game type.
	public static class FactoryCreator
	{
		// Creates the appropriate factory based on the selected game type.
		public static IGameFactory? CreateFactory(GameTypes gameType)
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