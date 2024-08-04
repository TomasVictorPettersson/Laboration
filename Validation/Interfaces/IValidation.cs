namespace Laboration.Validation.Interfaces
{
	public interface IValidation
	{
		// Validates the username and returns an error message if invalid.
		// Returns an empty string if valid.
		string ValidateUserName(string userName);

		// Checks if the provided username meets the required criteria.
		bool IsValidUserName(string userName);

		// Validates if the provided input is a valid 4-digit number with unique digits.
		bool IsInputValid(string input);

		// Checks if the provided guess matches the secret number.
		bool IsCorrectGuess(string guess, string secretNumber);
	}
}