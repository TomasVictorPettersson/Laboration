using Laboration.UI.Interfaces;
using Moq;

namespace Laboration.Tests.UI
{
	[TestClass]
	public class UserInterfaceTests
	{
		private readonly Mock<IUserInterface> _mockUserInterface = new Mock<IUserInterface>();

		[TestMethod]
		public void WelcomeMessage_WritesCorrectMessagesToConsole()
		{
			// Arrange
			const string userName = "JohnDoe";
			using StringWriter sw = new StringWriter();
			Console.SetOut(sw);

			// Act
			_mockUserInterface.Object.DisplayWelcomeMessage(userName);
			string consoleOutput = sw.ToString();

			// Assert
			StringAssert.Contains($"Welcome {userName} to Bulls and Cows!", consoleOutput);
			StringAssert.Contains("The objective is to guess a 4-digit number.", consoleOutput);
			StringAssert.Contains("Feedback: 'BBBB' for bulls (correct in position), 'CCCC' for cows (correct in wrong position).\n", consoleOutput);
		}

		[TestMethod]
		public void GetUserName_ValidUserName_ReturnsUserName()
		{
			// Arrange
			_mockUserInterface.SetupSequence(ui => ui.GetUserName())
				.Returns("ValidUser");

			// Act
			string userName = _mockUserInterface.Object.GetUserName();

			// Assert
			Assert.AreEqual("ValidUser", userName);
		}

		[TestMethod]
		public void GetUserName_EmptyUserName_ThrowsException()
		{
			// Arrange
			_mockUserInterface.Setup(ui => ui.GetUserName())
				.Throws(new InvalidOperationException("User name cannot be empty."));

			// Act & Assert
			Assert.ThrowsException<InvalidOperationException>(() => _mockUserInterface.Object.GetUserName());
		}

		[TestMethod]
		public void DisplayCorrectMessage_ValidData_DisplaysMessage()
		{
			// Arrange
			_mockUserInterface.Setup(ui => ui.DisplayCorrectMessage("1234", 5));

			// Act
			_mockUserInterface.Object.DisplayCorrectMessage("1234", 5);

			// Assert
			_mockUserInterface.Verify(ui => ui.DisplayCorrectMessage("1234", 5), Times.Once);
		}

		[TestMethod]
		public void AskToContinue_ValidYesInput_ReturnsTrue()
		{
			// Arrange
			_mockUserInterface.SetupSequence(ui => ui.AskToContinue())
				.Returns(true);

			// Act
			bool continueGame = _mockUserInterface.Object.AskToContinue();

			// Assert
			Assert.IsTrue(continueGame);
		}

		[TestMethod]
		public void AskToContinue_ValidNoInput_ReturnsFalse()
		{
			// Arrange
			_mockUserInterface.SetupSequence(ui => ui.AskToContinue())
				.Returns(false);

			// Act
			bool continueGame = _mockUserInterface.Object.AskToContinue();

			// Assert
			Assert.IsFalse(continueGame);
		}

		[TestMethod]
		public void AskToContinue_InvalidInput_ThrowsException()
		{
			// Arrange
			_mockUserInterface.Setup(ui => ui.AskToContinue())
				.Throws<InvalidOperationException>();

			// Act & Assert
			Assert.ThrowsException<InvalidOperationException>(() => _mockUserInterface.Object.AskToContinue());
		}
	}
}