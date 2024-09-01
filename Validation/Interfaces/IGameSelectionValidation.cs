using GameResources.Enums;

namespace Validation.Interfaces
{
	// Defines the contract for game selection validation.
	public interface IGameSelectionValidation
	{
		// Parses user input and returns corresponding GameType enum value.
		GameTypes? ParseGameTypeInput(string? input);
	}
}