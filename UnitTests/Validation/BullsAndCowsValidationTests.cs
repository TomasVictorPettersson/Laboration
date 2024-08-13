using Laboration.Validation.Implementations;

namespace Laboration.UnitTests.Validation
{
	[TestClass]
	public class BullsAndCowsValidationTests
	{
		private readonly BullsAndCowsValidation _validation = new();

		[TestMethod]
		public void ValidateUserName_ShouldReturnErrorForEmptyUserName()
		{
			// Arrange
			string userName = string.Empty;

			// Act
			string result = _validation.ValidateUserName(userName);

			// Assert
			Assert.AreEqual("Empty values are not allowed. Please enter a valid username.\n", result, "Expected error message for empty username.");
		}

		[TestMethod]
		public void ValidateUserName_ShouldReturnErrorForUserNameTooShort()
		{
			// Arrange

			const string userName = "A";

			// Act
			string result = _validation.ValidateUserName(userName);

			// Assert
			Assert.AreEqual("Username must be between 2 and 20 characters long.\n", result, "Expected error message for username too short.");
		}

		[TestMethod]
		public void ValidateUserName_ShouldReturnErrorForUserNameTooLong()
		{
			// Arrange
			string userName = new('A', 21);

			// Act
			string result = _validation.ValidateUserName(userName);

			// Assert
			Assert.AreEqual("Username must be between 2 and 20 characters long.\n", result, "Expected error message for username too long.");
		}

		[TestMethod]
		public void ValidateUserName_ShouldReturnEmptyForValidUserName()
		{
			// Arrange
			const string userName = "ValidUser";

			// Act
			string result = _validation.ValidateUserName(userName);

			// Assert
			Assert.AreEqual(string.Empty, result, "Expected no error message for a valid username.");
		}

		[TestMethod]
		public void IsValidUserName_ShouldReturnFalseForEmptyUserName()
		{
			// Arrange
			string userName = string.Empty;

			// Act
			bool result = _validation.IsValidUserName(userName);

			// Assert
			Assert.IsFalse(result, "Expected false for an empty username.");
		}

		[TestMethod]
		public void IsValidUserName_ShouldReturnFalseForUserNameTooShort()
		{
			// Arrange
			const string userName = "A";

			// Act
			bool result = _validation.IsValidUserName(userName);

			// Assert
			Assert.IsFalse(result, "Expected false for a username that is too short.");
		}

		[TestMethod]
		public void IsValidUserName_ShouldReturnFalseForUserNameTooLong()
		{
			// Arrange
			string userName = new('A', 21);

			// Act
			bool result = _validation.IsValidUserName(userName);

			// Assert
			Assert.IsFalse(result, "Expected false for a username that is too long.");
		}

		[TestMethod]
		public void IsValidUserName_ShouldReturnTrueForValidUserName()
		{
			// Arrange
			const string userName = "ValidUser";

			// Act
			bool result = _validation.IsValidUserName(userName);

			// Assert
			Assert.IsTrue(result, "Expected true for a valid username.");
		}

		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForNullInput()
		{
			// Arrange
			const string input = null!;

			// Act
			bool result = _validation.IsInputValid(input);

			// Assert
			Assert.IsFalse(result, "Expected false for null input.");
		}

		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForEmptyInput()
		{
			// Arrange
			string input = string.Empty;

			// Act
			bool result = _validation.IsInputValid(input);

			// Assert
			Assert.IsFalse(result, "Expected false for empty input.");
		}

		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForNonNumericInput()
		{
			// Arrange
			const string input = "text";

			// Act
			bool result = _validation.IsInputValid(input);

			// Assert
			Assert.IsFalse(result, "Expected false for non-numeric input.");
		}

		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForInputNot4Digits()
		{
			// Arrange
			const string input = "123";

			// Act
			bool result = _validation.IsInputValid(input);

			// Assert
			Assert.IsFalse(result, "Expected false for input that is not 4 digits long.");
		}

		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForRepeatingDigits()
		{
			// Arrange
			const string input = "1122";

			// Act
			bool result = _validation.IsInputValid(input);

			// Assert
			Assert.IsFalse(result, "Expected false for input with repeating digits.");
		}

		[TestMethod]
		public void IsInputValid_ShouldReturnTrueForValid4DigitUniqueNumber()
		{
			// Arrange
			const string input = "1234";

			// Act
			bool result = _validation.IsInputValid(input);

			// Assert
			Assert.IsTrue(result, "Expected true for a valid 4-digit unique number.");
		}

		[TestMethod]
		public void IsInputValid_InputWithNonNumericCharacters_ReturnsFalse()
		{
			const string input = "12A4";
			bool result = _validation.IsInputValid(input);
			Assert.IsFalse(result, "Expected false for input containing non-numeric characters.");
		}

		[TestMethod]
		public void IsInputValid_InputWithMixedCharacters_ReturnsFalse()
		{
			const string input = "1a34";
			bool result = _validation.IsInputValid(input);
			Assert.IsFalse(result, "Expected false for input containing a mix of numeric and non-numeric characters.");
		}

		[TestMethod]
		public void IsInputValid_InputWithSpecialCharacters_ReturnsFalse()
		{
			const string input = "12@4";
			bool result = _validation.IsInputValid(input);
			Assert.IsFalse(result, "Expected false for input containing special characters.");
		}

		[TestMethod]
		public void IsInputValid_InputWithLeadingOrTrailingSpaces_ReturnsTrue()
		{
			const string input = " 1234 ";
			bool result = _validation.IsInputValid(input.Trim());
			Assert.IsTrue(result, "Expected true for input with leading or trailing spaces when trimmed.");
		}

		[TestMethod]
		public void IsInputValid_InputWithSpacesInBetween_ReturnsFalse()
		{
			const string input = "12 34";
			bool result = _validation.IsInputValid(input);
			Assert.IsFalse(result, "Expected false for input containing spaces between digits.");
		}

		[TestMethod]
		public void IsInputValid_InputWithAllZeroes_ReturnsFalse()
		{
			// Arrange
			const string input = "0000";

			// Act
			bool result = _validation.IsInputValid(input);

			// Assert
			Assert.IsFalse(result, "Expected false for input with all zeroes.");
		}
	}
}