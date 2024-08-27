using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Interfaces
{
	// Interface defining the contract for a game factory creator.
	// Responsible for creating an instance of IGameFactory based on the specified game type.
	public interface IGameFactoryCreator
	{
		// Creates and returns a game factory corresponding to the provided GameTypes enum value.
		// Returns null if no factory exists for the given game type.
		IGameFactory? CreateGameFactory(GameTypes gameType);
	}
}