using GameResources.Constants;
using GameResources.Enums;
using Validation.Implementations;

namespace UnitTests.Validation
{
	[TestClass]
	public class MasterMindValidationTests
	{
		private readonly MasterMindValidation _validation = new();

		// Verifies that IsInputValid returns false for null input.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForNullInput()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, TestConstants.NullInput);

			// Assert
			Assert.IsFalse(result, "Expected false for null input.");
		}

		// Verifies that IsInputValid returns false for empty input.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForEmptyInput()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, TestConstants.EmptyInput);

			// Assert
			Assert.IsFalse(result, "Expected false for empty input.");
		}

		// Verifies that IsInputValid returns false for input that is not numeric.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForNonNumericInput()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, TestConstants.NonNumericInput);

			// Assert
			Assert.IsFalse(result, "Expected false for non-numeric input.");
		}

		// Verifies that IsInputValid returns false for input that is not exactly 4 digits long.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForInputNot4Digits()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, TestConstants.InputNot4Digits);

			// Assert
			Assert.IsFalse(result, "Expected false for input that is not 4 digits long.");
		}

		// Verifies that IsInputValid returns false for input with more than 4 digits.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForInputMoreThan4Digits()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, TestConstants.InputMoreThan4Digits);

			// Assert
			Assert.IsFalse(result, "Expected false for input with more than 4 digits.");
		}

		// Verifies that IsInputValid returns true for a valid 4-digit number.
		[TestMethod]
		public void IsInputValid_ShouldReturnTrueForValid4DigitNumber()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, TestConstants.Guess);

			// Assert
			Assert.IsTrue(result, "Expected true for a valid 4-digit number.");
		}

		// Verifies that IsInputValid returns true for input with repeating digits.
		[TestMethod]
		public void IsInputValid_ShouldReturnTrueForRepeatingDigits()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, TestConstants.RepeatingDigitsInput);

			// Assert
			Assert.IsTrue(result, "Expected true for input with repeating digits.");
		}

		// Verifies that IsInputValid returns false for input containing non-numeric characters.
		[TestMethod]
		public void IsInputValid_InputWithNonNumericCharacters_ReturnsFalse()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, TestConstants.InputWithNonNumericCharacters);

			// Assert
			Assert.IsFalse(result, "Expected false for input containing non-numeric characters.");
		}

		// Verifies that IsInputValid returns false for input containing a mix of numeric and non-numeric characters.
		[TestMethod]
		public void IsInputValid_InputWithMixedCharacters_ReturnsFalse()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, TestConstants.InputWithMixedCharacters);

			// Assert
			Assert.IsFalse(result, "Expected false for input containing a mix of numeric and non-numeric characters.");
		}

		// Verifies that IsInputValid returns false for input containing special characters.
		[TestMethod]
		public void IsInputValid_InputWithSpecialCharacters_ReturnsFalse()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, TestConstants.InputWithSpecialCharacters);

			// Assert
			Assert.IsFalse(result, "Expected false for input containing special characters.");
		}

		// Verifies that IsInputValid returns false for input with leading or trailing spaces.
		[TestMethod]
		public void IsInputValid_InputWithLeadingOrTrailingSpaces_ReturnsFalse()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, TestConstants.InputWithLeadingOrTrailingSpaces);

			// Assert
			Assert.IsFalse(result, "Expected false for input with leading or trailing spaces.");
		}

		// Verifies that IsInputValid returns false for input containing spaces between digits.
		[TestMethod]
		public void IsInputValid_InputWithSpacesInBetween_ReturnsFalse()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, TestConstants.InputWithSpacesInBetween);

			// Assert
			Assert.IsFalse(result, "Expected false for input containing spaces between digits.");
		}
	}
}