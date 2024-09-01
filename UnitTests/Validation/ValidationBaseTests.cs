using GameResources.Constants;
using Validation.Implementations;

namespace UnitTests.Validation
{
	[TestClass]
	public class ValidationBaseTests
	{
		private readonly BullsAndCowsValidation _validation = new();

		// Verifies that ValidateUserName returns an error message for an empty username.
		[TestMethod]
		public void ValidateUserName_ShouldReturnErrorForEmptyUserName()
		{
			// Arrange
			string userName = string.Empty;

			// Act
			string result = _validation.ValidateUserName(userName);

			// Assert
			Assert.AreEqual(UserInteractionMessages.EmptyUsernameMessage, result, "Expected error message for empty username.");
		}

		// Verifies that ValidateUserName returns an error message for a username that is too short.
		[TestMethod]
		public void ValidateUserName_ShouldReturnErrorForUserNameTooShort()
		{
			// Arrange
			const string userName = "A";

			// Act
			string result = _validation.ValidateUserName(userName);

			// Assert
			Assert.AreEqual(UserInteractionMessages.UsernameLengthMessage, result, "Expected error message for username too short.");
		}

		// Verifies that ValidateUserName returns an error message for a username that is too long.
		[TestMethod]
		public void ValidateUserName_ShouldReturnErrorForUserNameTooLong()
		{
			// Arrange
			string userName = new('A', 21);

			// Act
			string result = _validation.ValidateUserName(userName);

			// Assert
			Assert.AreEqual(UserInteractionMessages.UsernameLengthMessage, result, "Expected error message for username too long.");
		}

		// Verifies that ValidateUserName returns an empty string for a valid username.
		[TestMethod]
		public void ValidateUserName_ShouldReturnEmptyForValidUserName()
		{
			// Act
			string result = _validation.ValidateUserName(TestConstants.UserName);

			// Assert
			Assert.AreEqual(string.Empty, result, "Expected no error message for a valid username.");
		}

		// Verifies that IsValidUserName returns false for an empty username.
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

		// Verifies that IsValidUserName returns false for a username that is too short.
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

		// Verifies that IsValidUserName returns false for a username that is too long.
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

		// Verifies that IsValidUserName returns true for a valid username.
		[TestMethod]
		public void IsValidUserName_ShouldReturnTrueForValidUserName()
		{
			// Act
			bool result = _validation.IsValidUserName(TestConstants.UserName);

			// Assert
			Assert.IsTrue(result, "Expected true for a valid username.");
		}

		// Verifies that IsValidYesNoInput returns true for valid 'yes' input.
		[TestMethod]
		public void IsValidYesNoInput_ShouldReturnTrueForYesInput()
		{
			// Act
			bool result = _validation.IsValidYesNoInput(UserInputConstants.YesInput);

			// Assert
			Assert.IsTrue(result, "Expected true for valid 'yes' input.");
		}

		// Verifies that IsValidYesNoInput returns true for valid 'no' input.
		[TestMethod]
		public void IsValidYesNoInput_ShouldReturnTrueForNoInput()
		{
			// Act
			bool result = _validation.IsValidYesNoInput(UserInputConstants.NoInput);

			// Assert
			Assert.IsTrue(result, "Expected true for valid 'no' input.");
		}

		// Verifies that IsValidYesNoInput returns false for invalid input.
		[TestMethod]
		public void IsValidYesNoInput_ShouldReturnFalseForInvalidInput()
		{
			// Arrange
			const string input = "invalid";

			// Act
			bool result = _validation.IsValidYesNoInput(input);

			// Assert
			Assert.IsFalse(result, "Expected false for invalid input.");
		}
	}
}