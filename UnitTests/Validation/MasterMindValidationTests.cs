﻿using GameResources.Enums;
using Validation.Implementations;

namespace UnitTests.Validation
{
	[TestClass]
	public class MasterMindValidationTests
	{
		private MasterMindValidation _validation = new();

		[TestInitialize]
		public void Setup()
		{
			_validation = new MasterMindValidation();
		}

		// Verifies that IsInputValid returns false for null input.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForNullInput()
		{
			// Arrange
			const string input = null!;

			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, input);

			// Assert
			Assert.IsFalse(result, "Expected false for null input.");
		}

		// Verifies that IsInputValid returns false for empty input.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForEmptyInput()
		{
			// Arrange
			string input = string.Empty;

			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, input);

			// Assert
			Assert.IsFalse(result, "Expected false for empty input.");
		}

		// Verifies that IsInputValid returns false for input that is not numeric.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForNonNumericInput()
		{
			// Arrange
			const string input = "text";

			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, input);

			// Assert
			Assert.IsFalse(result, "Expected false for non-numeric input.");
		}

		// Verifies that IsInputValid returns false for input that is not exactly 4 digits long.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForInputNot4Digits()
		{
			// Arrange
			const string input = "123";

			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, input);

			// Assert
			Assert.IsFalse(result, "Expected false for input that is not 4 digits long.");
		}

		// Verifies that IsInputValid returns false for input with more than 4 digits.
		[TestMethod]
		public void IsInputValid_ShouldReturnFalseForInputMoreThan4Digits()
		{
			// Arrange
			const string input = "12345";

			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, input);

			// Assert
			Assert.IsFalse(result, "Expected false for input with more than 4 digits.");
		}

		// Verifies that IsInputValid returns true for a valid 4-digit number.
		[TestMethod]
		public void IsInputValid_ShouldReturnTrueForValid4DigitNumber()
		{
			// Arrange
			const string input = "1234";

			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, input);

			// Assert
			Assert.IsTrue(result, "Expected true for a valid 4-digit number.");
		}

		// Verifies that IsInputValid returns true for input with repeating digits.
		[TestMethod]
		public void IsInputValid_ShouldReturnTrueForRepeatingDigits()
		{
			// Arrange
			const string input = "1122";

			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, input);

			// Assert
			Assert.IsTrue(result, "Expected true for input with repeating digits.");
		}

		// Verifies that IsInputValid returns false for input containing non-numeric characters.
		[TestMethod]
		public void IsInputValid_InputWithNonNumericCharacters_ReturnsFalse()
		{
			// Arrange
			const string input = "12A4";

			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, input);

			// Assert
			Assert.IsFalse(result, "Expected false for input containing non-numeric characters.");
		}

		// Verifies that IsInputValid returns false for input containing a mix of numeric and non-numeric characters.
		[TestMethod]
		public void IsInputValid_InputWithMixedCharacters_ReturnsFalse()
		{
			// Arrange
			const string input = "1a34";

			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, input);

			// Assert
			Assert.IsFalse(result, "Expected false for input containing a mix of numeric and non-numeric characters.");
		}

		// Verifies that IsInputValid returns false for input containing special characters.
		[TestMethod]
		public void IsInputValid_InputWithSpecialCharacters_ReturnsFalse()
		{
			// Arrange
			const string input = "12@4";

			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, input);

			// Assert
			Assert.IsFalse(result, "Expected false for input containing special characters.");
		}

		// Verifies that IsInputValid returns false for input with leading or trailing spaces.
		[TestMethod]
		public void IsInputValid_InputWithLeadingOrTrailingSpaces_ReturnsFalse()
		{
			// Arrange
			const string input = " 1234 ";

			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, input);

			// Assert
			Assert.IsFalse(result, "Expected false for input with leading or trailing spaces.");
		}

		// Verifies that IsInputValid returns false for input containing spaces between digits.
		[TestMethod]
		public void IsInputValid_InputWithSpacesInBetween_ReturnsFalse()
		{
			// Arrange
			const string input = "12 34";

			// Act
			bool result = _validation.IsInputValid(GameTypes.MasterMind, input);

			// Assert
			Assert.IsFalse(result, "Expected false for input containing spaces between digits.");
		}
	}
}