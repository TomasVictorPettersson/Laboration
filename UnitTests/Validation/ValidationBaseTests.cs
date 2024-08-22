using Laboration.GameResources.Constants;
using Laboration.Validation.Implementations;

namespace Laboration.UnitTests.Validation
{
	[TestClass]
	public class ValidationBaseTests
	{
		private readonly ValidationBase _validation;

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
			// Arrange
			const string userName = "ValidUser";

			// Act
			string result = _validation.ValidateUserName(userName);

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
			// Arrange
			const string userName = "ValidUser";

			// Act
			bool result = _validation.IsValidUserName(userName);

			// Assert
			Assert.IsTrue(result, "Expected true for a valid username.");
		}
	}
}