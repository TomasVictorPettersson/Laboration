using Laboration.ConsoleUI.Implementations;
using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using Moq;

namespace Laboration.UnitTests.ConsoleUI
{
	[TestClass]
	public class MasterMindConsoleUITests
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
			_consoleUI = new MasterMindConsoleUI(_mockValidation.Object, _mockHighScoreManager.Object);

			_consoleOutput = new StringWriter();

			_originalConsoleOut = Console.Out;
			_originalConsoleIn = Console.In;

			Console.SetOut(_consoleOutput);
		}

		// Test case to ensure a detailed welcome message is displayed for a new game.
		[TestMethod]
		public void DisplayWelcomeMessage_ShouldPrintDetailedMessage_WhenNewGame()
		{
			// Arrange
			var expectedOutput = string.Format(GameMessages.MasterMindWelcomeMessageFormat, TestConstants.UserName);

			// Act
			_consoleUI.DisplayWelcomeMessage(GameTypes.MasterMind, TestConstants.UserName, true);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The detailed welcome message for a new game was not printed correctly.");
		}

		// Test case to ensure a brief welcome message is displayed for a returning player.
		[TestMethod]
		public void DisplayWelcomeMessage_ShouldPrintBriefMessage_WhenReturningPlayer()
		{
			// Arrange
			var expectedOutput = string.Format(GameMessages.WelcomeBackMessageFormat, TestConstants.UserName);

			// Act
			_consoleUI.DisplayWelcomeMessage(GameTypes.MasterMind, TestConstants.UserName, false);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The brief welcome message for a returning player was not printed correctly.");
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