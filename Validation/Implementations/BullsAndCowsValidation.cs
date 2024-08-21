using Laboration.GameResources.Enums;

namespace Laboration.Validation.Implementations
{
	// Validation for "Bulls and Cows" game.
	public class BullsAndCowsValidation : ValidationBase
	{
		// Validates that the input is a 4-digit number with unique digits.
		public override bool IsInputValid(GameTypes gameType, string input)
		{
			if (gameType == GameTypes.BullsAndCows)
			{
				return !string.IsNullOrEmpty(input)
					   && input.Length == 4
					   && int.TryParse(input, out _)
					   && input.Distinct().Count() == 4;
			}

			return false;
		}
	}
}