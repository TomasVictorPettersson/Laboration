using GameResources.Constants;
using GameResources.Enums;

namespace Validation.Implementations
{
	// Validation for the "MasterMind" game.
	public class MasterMindValidation : ValidationBase
	{
		// Checks if the input is a
		// -digit number.
		public override bool IsInputValid(GameTypes gameType, string input)
		{
			if (gameType == GameTypes.MasterMind)
			{
				return !string.IsNullOrEmpty(input)
					   && input.Length == GameConstants.SecretNumberLength
					   && int.TryParse(input, out _);
			}
			return false;
		}
	}
}