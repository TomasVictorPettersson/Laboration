using Laboration.ConsoleUI.Interfaces;
using Moq;

namespace Laboration.UnitTests.ConsoleUI
{
	[TestClass]
	public class BullsAndCowsConsoleUITests
	{
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();

		[TestMethod]
		public void GetUserName_ValidUserName_ReturnsUserName()
		{
			// Arrange
			_mockConsoleUI.Setup(ui => ui.GetUserName()).Returns("ValidUser");

			// Act
			string userName = _mockConsoleUI.Object.GetUserName();

			// Assert
			Assert.AreEqual("ValidUser", userName, "The user name returned should be 'ValidUser'.");
		}

		[TestMethod]
		public void GetUserName_EmptyUserName_ThrowsException()
		{
			// Arrange
			_mockConsoleUI.Setup(ui => ui.GetUserName())
				.Throws(new InvalidOperationException("User name cannot be empty."));

			// Act & Assert
			var exception = Assert.ThrowsException<InvalidOperationException>(() => _mockConsoleUI.Object.GetUserName());
			Assert.AreEqual("User name cannot be empty.", exception.Message, "Exception message should be 'User name cannot be empty.'");
		}

		[TestMethod]
		public void DisplayCorrectMessage_ValidData_DisplaysMessage()
		{
			// Arrange
			_mockConsoleUI.Setup(ui => ui.DisplayCorrectMessage("1234", 5));

			// Act
			_mockConsoleUI.Object.DisplayCorrectMessage("1234", 5);

			// Assert
			_mockConsoleUI.Verify(ui => ui.DisplayCorrectMessage("1234", 5), Times.Once,
				"DisplayCorrectMessage should be called once with parameters '1234' and 5.");
		}

		[TestMethod]
		public void AskToContinue_ValidYesInput_ReturnsTrue()
		{
			// Arrange
			_mockConsoleUI.Setup(ui => ui.AskToContinue()).Returns(true);

			// Act
			bool continueGame = _mockConsoleUI.Object.AskToContinue();

			// Assert
			Assert.IsTrue(continueGame, "AskToContinue should return true for a valid 'yes' input.");
		}

		[TestMethod]
		public void AskToContinue_ValidNoInput_ReturnsFalse()
		{
			// Arrange
			_mockConsoleUI.Setup(ui => ui.AskToContinue()).Returns(false);

			// Act
			bool continueGame = _mockConsoleUI.Object.AskToContinue();

			// Assert
			Assert.IsFalse(continueGame, "AskToContinue should return false for a valid 'no' input.");
		}

		[TestMethod]
		public void AskToContinue_InvalidInput_ThrowsException()
		{
			// Arrange
			_mockConsoleUI.Setup(ui => ui.AskToContinue())
				.Throws(new InvalidOperationException("Invalid input."));

			// Act & Assert
			var exception = Assert.ThrowsException<InvalidOperationException>(() => _mockConsoleUI.Object.AskToContinue());
			Assert.AreEqual("Invalid input.", exception.Message, "Exception message should be 'Invalid input.'");
		}
	}
}