using Laboration.Validation.Interfaces;

namespace Laboration.Validation.Implementations
{
	// Provides validation functionality for the Bulls and Cows game.
	public class BullsAndCowsValidation : IValidation
	{
		// Validates the username and returns an error message if invalid.
		// Returns an empty string if valid.
		public string ValidateUserName(string userName)
		{
			if (string.IsNullOrEmpty(userName))
			{
				return "Empty values are not allowed. Please enter a valid username.\n";
			}
			else if (userName.Length < 6 || userName.Length > 30)
			{
				return "Username must be between 6 and 30 characters long.\n";
			}
			return string.Empty;
		}

		// Checks if the username is valid.
		public bool IsValidUserName(string userName)
		{
			return !string.IsNullOrEmpty(userName) && userName.Length >= 6 && userName.Length <= 30;
		}

		// Validates if the input is a valid 4-digit number with unique digits.
		public bool IsInputValid(string input)
		{
			return !string.IsNullOrEmpty(input) && input.Length == 4 && int.TryParse(input, out _) && input.Distinct().Count() == 4;
		}

		// Checks if the player's guess matches the secret number.
		public bool IsCorrectGuess(string guess, string secretNumber)
		{
			try
			{
				return string.Equals(guess, secretNumber, StringComparison.OrdinalIgnoreCase);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error checking guess correctness: {ex.Message}");
				throw;
			}
		}
	}
}