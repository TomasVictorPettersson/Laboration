using Laboration.GameResources.Enums;

namespace Laboration.Validation.Implementations
{
	// Validation for the "MasterMind" game.
	public class MasterMindValidation : ValidationBase
	{
		// Checks if the input is a 4-digit number.
		public override bool IsInputValid(GameTypes gameType, string input)
		{
			if (gameType == GameTypes.MasterMind)
			{
				return !string.IsNullOrEmpty(input)
					   && input.Length == 4
					   && int.TryParse(input, out _);
			}
			return false;
		}
	}
}