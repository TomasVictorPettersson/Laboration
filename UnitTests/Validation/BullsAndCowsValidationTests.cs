using GameResources.Constants;
using GameResources.Enums;
using Validation.Implementations;

namespace UnitTests.Validation
{
	[TestClass]
	public class BullsAndCowsValidationTests
	{
		private readonly BullsAndCowsValidation _validation = new();

		// Verifies that IsInputValid returns false for null input.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForNullInput()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.NullInput);

			// Assert
			Assert.IsFalse(result, "Expected false for null input.");
		}

		// Verifies that IsInputValid returns false for empty input.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForEmptyInput()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.EmptyInput);

			// Assert
			Assert.IsFalse(result, "Expected false for empty input.");
		}

		// Verifies that IsInputValid returns false for input that is not numeric.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForNonNumericInput()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.NonNumericInput);

			// Assert
			Assert.IsFalse(result, "Expected false for non-numeric input.");
		}

		// Verifies that IsInputValid returns false for input that is not exactly 4 digits long.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForInputNot4Digits()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.InputNot4Digits);

			// Assert
			Assert.IsFalse(result, "Expected false for input that is not 4 digits long.");
		}

		// Verifies that IsInputValid returns false for input with more than 4 digits.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForInputMoreThan4Digits()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.InputMoreThan4Digits);

			// Assert
			Assert.IsFalse(result, "Expected false for input with more than 4 digits.");
		}

		// Verifies that IsInputValid returns false for input with repeating digits.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForRepeatingDigits()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.RepeatingDigitsInput);

			// Assert
			Assert.IsFalse(result, "Expected false for input with repeating digits.");
		}

		// Verifies that IsInputValid returns true for a valid 4-digit unique number.
		[TestMethod]
		public void IsInputValid_ShouldReturnTrueForValid4DigitUniqueNumber()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.Valid4DigitUniqueNumber);

			// Assert
			Assert.IsTrue(result, "Expected true for a valid 4-digit unique number.");
		}

		// Verifies that IsInputValid returns false for input containing non-numeric characters.
		[TestMethod]
		public void IsInputValid_InputWithNonNumericCharacters_ReturnsFalse()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.InputWithNonNumericCharacters);

			// Assert
			Assert.IsFalse(result, "Expected false for input containing non-numeric characters.");
		}

		// Verifies that IsInputValid returns false for input containing a mix of numeric and non-numeric characters.
		[TestMethod]
		public void IsInputValid_InputWithMixedCharacters_ReturnsFalse()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.InputWithMixedCharacters);

			// Assert
			Assert.IsFalse(result, "Expected false for input containing a mix of numeric and non-numeric characters.");
		}

		// Verifies that IsInputValid returns false for input containing special characters.
		[TestMethod]
		public void IsInputValid_InputWithSpecialCharacters_ReturnsFalse()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.InputWithSpecialCharacters);

			// Assert
			Assert.IsFalse(result, "Expected false for input containing special characters.");
		}

		// Verifies that IsInputValid returns true for input with leading or trailing spaces when trimmed.
		[TestMethod]
		public void IsInputValid_InputWithLeadingOrTrailingSpaces_ReturnsTrue()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.InputWithLeadingOrTrailingSpaces.Trim());

			// Assert
			Assert.IsTrue(result, "Expected true for input with leading or trailing spaces when trimmed.");
		}

		// Verifies that IsInputValid returns false for input containing spaces between digits.
		[TestMethod]
		public void IsInputValid_InputWithSpacesInBetween_ReturnsFalse()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.InputWithSpacesInBetween);

			// Assert
			Assert.IsFalse(result, "Expected false for input containing spaces between digits.");
		}

		// Verifies that IsInputValid returns false for input consisting of all zeroes.
		[TestMethod]
		public void IsInputValid_InputWithAllZeroes_ReturnsFalse()
		{
			// Act
			bool result = _validation.IsInputValid(GameTypes.BullsAndCows, TestConstants.InputWithAllZeroes);

			// Assert
			Assert.IsFalse(result, "Expected false for input with all zeroes.");
		}
	}
}