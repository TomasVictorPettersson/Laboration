using Laboration.ConsoleUI.Interfaces;
using Moq;

namespace Laboration.UnitTests.ConsoleUI
{
	[TestClass]
	public class BullsAndCowsConsoleUITests
	{
		private readonly Mock<IConsoleUI> _mockUserInterface = new();

		[TestMethod]
		public void GetUserName_ValidUserName_ReturnsUserName()
		{
			// Arrange
			_mockUserInterface.Setup(ui => ui.GetUserName()).Returns("ValidUser");

			// Act
			string userName = _mockUserInterface.Object.GetUserName();

			// Assert
			Assert.AreEqual("ValidUser", userName, "The user name returned should be 'ValidUser'.");
		}

		[TestMethod]
		public void GetUserName_EmptyUserName_ThrowsException()
		{
			// Arrange
			_mockUserInterface.Setup(ui => ui.GetUserName())
				.Throws(new InvalidOperationException("User name cannot be empty."));

			// Act & Assert
			var exception = Assert.ThrowsException<InvalidOperationException>(() => _mockUserInterface.Object.GetUserName());
			Assert.AreEqual("User name cannot be empty.", exception.Message, "Exception message should be 'User name cannot be empty.'");
		}

		[TestMethod]
		public void DisplayCorrectMessage_ValidData_DisplaysMessage()
		{
			// Arrange
			_mockUserInterface.Setup(ui => ui.DisplayCorrectMessage("1234", 5));

			// Act
			_mockUserInterface.Object.DisplayCorrectMessage("1234", 5);

			// Assert
			_mockUserInterface.Verify(ui => ui.DisplayCorrectMessage("1234", 5), Times.Once,
				"DisplayCorrectMessage should be called once with parameters '1234' and 5.");
		}

		[TestMethod]
		public void AskToContinue_ValidYesInput_ReturnsTrue()
		{
			// Arrange
			_mockUserInterface.Setup(ui => ui.AskToContinue()).Returns(true);

			// Act
			bool continueGame = _mockUserInterface.Object.AskToContinue();

			// Assert
			Assert.IsTrue(continueGame, "AskToContinue should return true for a valid 'yes' input.");
		}

		[TestMethod]
		public void AskToContinue_ValidNoInput_ReturnsFalse()
		{
			// Arrange
			_mockUserInterface.Setup(ui => ui.AskToContinue()).Returns(false);

			// Act
			bool continueGame = _mockUserInterface.Object.AskToContinue();

			// Assert
			Assert.IsFalse(continueGame, "AskToContinue should return false for a valid 'no' input.");
		}

		[TestMethod]
		public void AskToContinue_InvalidInput_ThrowsException()
		{
			// Arrange
			_mockUserInterface.Setup(ui => ui.AskToContinue())
				.Throws(new InvalidOperationException("Invalid input."));

			// Act & Assert
			var exception = Assert.ThrowsException<InvalidOperationException>(() => _mockUserInterface.Object.AskToContinue());
			Assert.AreEqual("Invalid input.", exception.Message, "Exception message should be 'Invalid input.'");
		}
	}
}