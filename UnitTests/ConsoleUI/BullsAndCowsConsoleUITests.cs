using ConsoleUI.Implementations;
using GameResources.Constants;
using GameResources.Enums;
using HighScoreManagement.Interfaces;
using Moq;
using Validation.Interfaces;

namespace UnitTests.ConsoleUI
{
	[TestClass]
	public class BullsAndCowsConsoleUITests
	{
		private readonly Mock<IValidation> _mockValidation = new();
		private readonly Mock<IHighScoreManager> _mockHighScoreManager = new();

		private ConsoleUIBase _consoleUI = null!;

		private StringWriter _consoleOutput = new();

		private TextWriter _originalConsoleOut = null!;
		private TextReader _originalConsoleIn = null!;

		// Initialize test setup before each test method is run.
		[TestInitialize]
		public void Setup()
		{
			_consoleUI = new BullsAndCowsConsoleUI(_mockValidation.Object, _mockHighScoreManager.Object);

			_consoleOutput = new StringWriter();

			_originalConsoleOut = Console.Out;
			_originalConsoleIn = Console.In;

			Console.SetOut(_consoleOutput);
		}

		// Test case to ensure detailed welcome message is printed for a new game.
		[TestMethod]
		public void DisplayWelcomeMessage_ShouldPrintDetailedMessage_WhenNewGame()
		{
			// Arrange
			var expectedOutput = string.Format(GameMessages.BullsAndCowsWelcomeMessageFormat, TestConstants.UserName);

			// Act
			_consoleUI.DisplayWelcomeMessage(GameTypes.BullsAndCows, TestConstants.UserName, true);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The welcome message for a new game is not correct.");
		}

		// Test case to ensure brief welcome message is printed for a returning player.
		[TestMethod]
		public void DisplayWelcomeMessage_ShouldPrintBriefMessage_WhenReturningPlayer()
		{
			// Arrange
			var expectedOutput = string.Format(GameMessages.WelcomeBackMessageFormat, TestConstants.UserName);

			// Act
			_consoleUI.DisplayWelcomeMessage(GameTypes.BullsAndCows, TestConstants.UserName, false);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The welcome message for a returning player is not correct.");
		}

		// Cleanup after each test method is run.
		[TestCleanup]
		public void Cleanup()
		{
			Console.SetOut(_originalConsoleOut);
			Console.SetIn(_originalConsoleIn);

			_consoleOutput.Dispose();
		}
	}
}