using GameResources.Enums;

namespace ConsoleUI.Interfaces
{
	// Interface for selecting game types
	public interface IGameSelector
	{
		// Method to prompt the user and return the selected game type
		GameTypes SelectGameType();

		// Method to show game options and formatting
		void DisplayGameOptions();

		// Method that parses user input and returns corresponding GameType enum value
		GameTypes? ParseUserInput(string? input);
	}
}