using Laboration.GameResources.Enums;

namespace Laboration.ConsoleUI.Interfaces
{
	// Interface for selecting game types
	public interface IGameSelector
	{
		// Method to prompt the user and return the selected game type
		GameTypes SelectGameType();
	}
}