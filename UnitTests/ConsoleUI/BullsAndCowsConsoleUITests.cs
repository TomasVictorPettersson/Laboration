using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using Moq;

namespace Laboration.UnitTests.ConsoleUI
{
	[TestClass]
	public class BullsAndCowsConsoleUITests
	{
		private Mock<IValidation> _mockValidation;
		private Mock<IHighScoreManager> _mockHighScoreManager;
		private BullsAndCowsConsoleUI _consoleUI;
		private StringWriter _consoleOutput;
		private TextWriter _originalConsoleOut;
		private TextReader _originalConsoleIn;
		private Mock<IConsoleUI> _mockConsoleUI;

		[TestInitialize]
		public void Setup()
		{
			_mockValidation = new Mock<IValidation>();
			_mockHighScoreManager = new Mock<IHighScoreManager>();
			_mockConsoleUI = new Mock<IConsoleUI>();
			_consoleUI = new BullsAndCowsConsoleUI(_mockValidation.Object, _mockHighScoreManager.Object);
			_originalConsoleOut = Console.Out;
			_originalConsoleIn = Console.In;
			_consoleOutput = new StringWriter();
			Console.SetOut(_consoleOutput);
		}

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
			string secretNumber = "1234";
			int numberOfGuesses = 5;

			// Act
			_consoleUI.DisplayCorrectMessage(secretNumber, numberOfGuesses);

			// Assert
			var expectedOutput = $"Correct! The secret number was: {secretNumber}\nIt took you {numberOfGuesses} guesses.";
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The message should match the expected output.");
		}

		[TestMethod]
		public void TestDisplayWelcomeMessage()
		{
			// Arrange
			string userName = "TestUser";
			string expectedOutput =
				$"Welcome {userName} to Bulls and Cows!\n" +
				"\nThe objective of the game is to guess a 4-digit number.\n" +
				"Each digit in the 4-digit number will only appear once.\n" +
				"\nFor each guess, you will receive feedback in the form of 'BBBB,CCCC',\n" +
				"where 'BBBB' represents the number of bulls (correct digits in the correct positions),\n" +
				"and 'CCCC' represents the number of cows (correct digits in the wrong positions).\n" +
				"If you receive a response of only ',' it means none of the digits in your guess are present in the 4-digit number.\n\n" +
				"New game:";

			var consoleOutput = new StringWriter();
			Console.SetOut(consoleOutput);

			// Act
			_consoleUI.DisplayWelcomeMessage(userName);

			// Assert
			string actualOutput = consoleOutput.ToString().Trim();
			Assert.AreEqual(expectedOutput, actualOutput);
		}

		[TestMethod]
		public void DisplaySecretNumberForPractice_ShouldPrintSecretNumber()
		{
			// Arrange
			var secretNumber = "1234";

			// Act
			_consoleUI.DisplaySecretNumberForPractice(secretNumber);

			// Assert
			Assert.AreEqual($"For practice, number is: {secretNumber}", _consoleOutput.ToString().Trim(), "The secret number message should match the expected output.");
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
			_mockConsoleUI.Setup(ui => ui.AskToContinue()).Throws(new InvalidOperationException("Invalid input."));

			// Act & Assert
			var exception = Assert.ThrowsException<InvalidOperationException>(() => _mockConsoleUI.Object.AskToContinue());
			Assert.AreEqual("Invalid input.", exception.Message, "Exception message should be 'Invalid input.'");
		}

		[TestMethod]
		public void DisplayGoodbyeMessage_ShouldPrintGoodbyeMessage()
		{
			// Arrange
			string userName = "testUser";

			// Act
			_consoleUI.DisplayGoodbyeMessage(userName);

			// Assert
			var expectedOutput = $"Thank you, {userName}, for playing Bulls and Cows!";
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The goodbye message should match the expected output.");
		}

		[TestCleanup]
		public void Cleanup()
		{
			try
			{
				Console.SetOut(_originalConsoleOut);
				Console.SetIn(_originalConsoleIn);
			}
			finally
			{
				_consoleOutput.Dispose();
			}
		}
	}
}