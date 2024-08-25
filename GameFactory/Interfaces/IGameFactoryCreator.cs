using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Interfaces
{
	// Defines the contract for creating game factories.
	public interface IGameFactoryCreator
	{
		IGameFactory? CreateGameFactory(GameTypes gameType);
	}
}