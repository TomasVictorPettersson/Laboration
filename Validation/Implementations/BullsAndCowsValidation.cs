using Laboration.Validation.Interfaces;

namespace Laboration.Validation.Implementations
{
	public class BullsAndCowsValidation : IValidation
	{
		// Validates username and prints error messages if invalid.
		public string ValidateUserName(string userName)
		{
			if (string.IsNullOrEmpty(userName))
			{
				return "Empty values are not allowed. Please enter a valid username.";
			}
			else if (userName.Length < 2 || userName.Length > 20)
			{
				return "Username must be between 2 and 20 characters long.";
			}
			return string.Empty;
		}

		// Checks if the username is valid.
		public bool IsValidUserName(string userName)
		{
			return !string.IsNullOrEmpty(userName) && userName.Length >= 2 && userName.Length <= 20;
		}

		// Validates if the input is a valid 4-digit number with unique digits.
		public bool IsInputValid(string input)
		{
			return !string.IsNullOrEmpty(input) && input.Length == 4 && int.TryParse(input, out _) && input.Distinct().Count() == 4;
		}
	}
}