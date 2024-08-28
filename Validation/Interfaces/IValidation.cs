using GameResources.Enums;

namespace Validation.Interfaces
{
	// Defines the contract for validation functionality.
	public interface IValidation
	{
		// Validates the username and returns an error message if invalid; otherwise, returns an empty string.
		string ValidateUserName(string userName);

		// Checks if the provided username meets the required criteria.
		bool IsValidUserName(string userName);

		// Validates if the input is a 4-digit number with unique digits.
		bool IsInputValid(GameTypes gameType, string input);

		// Checks if the provided guess matches the secret number.
		bool IsCorrectGuess(string guess, string secretNumber);
	}
}