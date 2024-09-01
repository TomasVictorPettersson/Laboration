using GameResources.Enums;
using Validation.Interfaces;

namespace Validation.Implementations
{
	// Handles validation for game selection inputs.
	public class GameSelectionValidation : IGameSelectionValidation
	{
		// Parses user input and returns corresponding GameType enum value
		public GameTypes? ParseGameTypeInput(string? input)
		{
			return input switch
			{
				"1" => GameTypes.BullsAndCows, // Option 1: Bulls and Cows game
				"2" => GameTypes.MasterMind,   // Option 2: MasterMind game
				"3" => GameTypes.Quit,         // Option 3: Exit the application
				_ => null                      // Invalid input
			};
		}
	}
}