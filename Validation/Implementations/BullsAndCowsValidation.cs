using GameResources.Constants;
using GameResources.Enums;

namespace Validation.Implementations
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
					   && input.Length == GameConstants.SecretNumberLength
					   && int.TryParse(input, out _)
					   && input.Distinct().Count() == GameConstants.SecretNumberLength;
			}

			return false;
		}
	}
}