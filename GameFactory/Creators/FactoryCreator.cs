using Laboration.GameFactory.Implementations;
using Laboration.GameFactory.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Creators
{
	public static class FactoryCreator
	{
		// Creates the appropriate factory based on the selected game type.
		public static IGameFactory? CreateFactory(GameTypes gameType)
		{
			return gameType switch
			{
				GameTypes.BullsAndCows => new GameComponentFactory(GameTypes.BullsAndCows),
				GameTypes.MasterMind => new GameComponentFactory(GameTypes.MasterMind),
				GameTypes.Quit => null, // Explicitly handle the Quit game type by returning null
				_ => throw new ArgumentException("Invalid game type", nameof(gameType)),
			};
		}
	}
}