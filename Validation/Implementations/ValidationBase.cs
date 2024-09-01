using GameResources.Constants;
using GameResources.Enums;
using Validation.Interfaces;

namespace Validation.Implementations
{
	public abstract class ValidationBase : IValidation
	{
		// Validates the username and returns an error message if invalid.
		// Returns an empty string if valid.
		public string ValidateUserName(string userName)
		{
			if (string.IsNullOrEmpty(userName))
			{
				return UserInteractionMessages.EmptyUsernameMessage;
			}
			else if (userName.Length < UserValidationConstants.UserNameMinLength ||
					 userName.Length > UserValidationConstants.UserNameMaxLength)
			{
				return UserInteractionMessages.UsernameLengthMessage;
			}
			return string.Empty;
		}

		// Checks if the username is valid.
		public bool IsValidUserName(string userName)
		{
			return !string.IsNullOrEmpty(userName) &&
				   userName.Length >= UserValidationConstants.UserNameMinLength &&
				   userName.Length <= UserValidationConstants.UserNameMaxLength;
		}

		// Checks if the player's guess matches the secret number.
		public bool IsCorrectGuess(string guess, string secretNumber)
		{
			return string.Equals(guess, secretNumber, StringComparison.OrdinalIgnoreCase);
		}

		// Abstract method for game-specific input validation.
		public abstract bool IsInputValid(GameTypes gameType, string input);

		// Validates yes/no input from the user.
		public bool IsValidYesNoInput(string input)
		{
			return input == UserInputConstants.YesInput || input == UserInputConstants.NoInput;
		}
	}
}