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
		private Mock<IValidation> _mockValidation;
		private Mock<IHighScoreManager> _mockHighScoreManager;
		private ConsoleUIBase _consoleUI;
		private StringWriter _consoleOutput;
		private TextWriter _originalConsoleOut;
		private TextReader _originalConsoleIn;

		[TestInitialize]
		public void Setup()
		{
			_mockValidation = new Mock<IValidation>();
			_mockHighScoreManager = new Mock<IHighScoreManager>();
			_consoleUI = new TestConsoleUI(_mockValidation.Object, _mockHighScoreManager.Object);
			_consoleOutput = new StringWriter();
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

		[TestMethod]
		public void DisplayGoodbyeMessage_ShouldPrintCorrectMessage()
		{
			var expectedOutput = string.Format(GameMessages.MasterMindGoodbyeMessageFormat, TestConstants.UserName);
			_consoleUI.DisplayGoodbyeMessage(GameTypes.MasterMind, TestConstants.UserName);
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
		private class TestConsoleUI : ConsoleUIBase
		{
			public TestConsoleUI(IValidation validation, IHighScoreManager highScoreManager)
				: base(validation, highScoreManager) { }

			public override string GetWelcomeMessageFormat(GameTypes gameType)
				=> GameMessages.MasterMindWelcomeMessageFormat;

			public override string GetGoodbyeMessageFormat(GameTypes gameType)
				=> GameMessages.MasterMindGoodbyeMessageFormat;

			public override void DisplayInvalidInputMessage() => Console.WriteLine("Invalid input. Please try again.");
		}
	}
}