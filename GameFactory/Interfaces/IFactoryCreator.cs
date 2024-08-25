using Laboration.GameFactory.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Interfaces
{
	// Defines the contract for creating game factories.
	public interface IFactoryCreator
	{
		IGameFactory? CreateFactory(GameTypes gameType);
	}
}