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
		private readonly StringWriter _consoleOutput = new();
		private TextWriter _originalConsoleOut = null!;
		private TextReader _originalConsoleIn = null!;

		[TestInitialize]
		public void Setup()
		{
			_consoleUI = new TestConsoleUI(_mockValidation.Object, _mockHighScoreManager.Object);
			_originalConsoleOut = Console.Out;
			_originalConsoleIn = Console.In;
			Console.SetOut(_consoleOutput);
		}

		[TestMethod]
		public void DisplayWelcomeMessage_ShouldPrintDetailedMessage_WhenNewGame()
		{
			var expectedOutput = string.Format(GameMessages.MasterMindWelcomeMessageFormat, TestConstants.UserName);
			_consoleUI.DisplayWelcomeMessage(GameTypes.MasterMind, TestConstants.UserName, true);
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim());
		}

		[TestMethod]
		public void DisplayWelcomeMessage_ShouldPrintBriefMessage_WhenReturningPlayer()
		{
			var expectedOutput = string.Format(GameMessages.WelcomeBackMessageFormat, TestConstants.UserName);
			_consoleUI.DisplayWelcomeMessage(GameTypes.MasterMind, TestConstants.UserName, false);
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim());
		}

		[TestCleanup]
		public void Cleanup()
		{
			Console.SetOut(_originalConsoleOut);
			Console.SetIn(_originalConsoleIn);
			_consoleOutput.Dispose();
		}

		// Derived class for testing abstract methods
		private class TestConsoleUI(IValidation validation, IHighScoreManager highScoreManager) : ConsoleUIBase(validation, highScoreManager)
		{
			public override string GetWelcomeMessageFormat(GameTypes gameType)
				=> GameMessages.MasterMindWelcomeMessageFormat;

			public override string GetGoodbyeMessageFormat(GameTypes gameType)
				=> GameMessages.MasterMindGoodbyeMessageFormat;

			public override void DisplayInvalidInputMessage() => Console.WriteLine("Invalid input. Please try again.");
		}
	}
}