using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;
using Laboration.Validation.Interfaces;

namespace Laboration.Validation.Implementations
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
			else if (userName.Length < 2 || userName.Length > 20)
			{
				return UserInteractionMessages.UsernameLengthMessage;
			}
			return string.Empty;
		}

		// Checks if the username is valid.
		public bool IsValidUserName(string userName)
		{
			return !string.IsNullOrEmpty(userName) && userName.Length >= 2 && userName.Length <= 20;
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

		// Abstract method for game-specific input validation.
		public abstract bool IsInputValid(GameTypes gameType, string input);
	}
}