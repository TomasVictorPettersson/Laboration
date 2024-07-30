namespace Laboration.Validation.Interfaces
{
	public interface IValidation
	{
		// Validates the provided username and prints error messages if invalid.
		string ValidateUserName(string userName);

		// Checks if the provided username meets the required criteria.
		bool IsValidUserName(string userName);

		// Validates if the provided input is a valid 4-digit number with unique digits.
		bool IsInputValid(string input);
	}
}