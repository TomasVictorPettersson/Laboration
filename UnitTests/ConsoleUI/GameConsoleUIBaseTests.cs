using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Mocks;
using Laboration.PlayerData.Interfaces;
using Laboration.Validation.Interfaces;
using Moq;

namespace Laboration.UnitTests.ConsoleUI
{
	[TestClass]
	public class GameConsoleUIBaseTests
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
			_consoleUI = CreateTestConsoleUI();

			_originalConsoleOut = Console.Out;
			_originalConsoleIn = Console.In;
			Console.SetOut(_consoleOutput);
		}

		[TestMethod]
		public void DisplaySecretNumberForPractice_ShouldPrintSecretNumber()
		{
			var expectedOutput = string.Format(GameMessages.SecretNumberPracticeMessage, TestConstants.SecretNumber);
			_consoleUI.DisplaySecretNumberForPractice(TestConstants.SecretNumber);
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim());
		}

		[TestMethod]
		public void DisplayCorrectMessage_SingleGuess_DisplaysMessage()
		{
			var expectedOutput = string.Format(GameMessages.CorrectGuessMessageFormat, TestConstants.SecretNumber, TestConstants.SingleGuess, Plurals.GuessSingular);
			_consoleUI.DisplayCorrectMessage(TestConstants.SecretNumber, TestConstants.SingleGuess);
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim());
		}

		[TestMethod]
		public void DisplayCorrectMessage_MultipleGuesses_DisplaysMessage()
		{
			var expectedOutput = string.Format(GameMessages.CorrectGuessMessageFormat, TestConstants.SecretNumber, TestConstants.MultipleGuesses, Plurals.GuessPlural);
			_consoleUI.DisplayCorrectMessage(TestConstants.SecretNumber, TestConstants.MultipleGuesses);
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim());
		}

		[TestMethod]
		public void DisplayHighScoreList_ShouldDisplayHighScoreList()
		{
			var expectedResults = new List<IPlayerData>
			{
				MockPlayerData.CreateMock()
			};
			_mockHighScoreManager.Setup(m => m.ReadHighScoreResultsFromFile()).Returns(expectedResults);
			_consoleUI.DisplayHighScoreList(TestConstants.UserName);
			_mockHighScoreManager.Verify(m => m.ReadHighScoreResultsFromFile(), Times.Once);
			_mockHighScoreManager.Verify(m => m.SortHighScoreList(expectedResults), Times.Once);
		}

		[TestMethod]
		public void DisplayRank_ShouldFormatRankCorrectly()
		{
			var expectedOutput = string.Format("{0,-" + FormattingConstants.RankColumnWidth + "}", TestConstants.Rank);
			_consoleUI.DisplayRank(int.Parse(TestConstants.Rank));
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString());
		}

		[TestMethod]
		public void DisplayPlayerData_ShouldPrintPlayerDataWithCorrectFormatting()
		{
			var player = MockPlayerData.CreateMock();
			_consoleUI.DisplayPlayerData(player, true, TestConstants.MaxUserNameLength);
			var expectedOutput = string.Format(
				"{0,-" + TestConstants.MaxUserNameLength + "} {1," + FormattingConstants.GamesPlayedColumnWidth + "} {2," + FormattingConstants.AverageGuessesColumnWidth + "}",
				TestConstants.UserName,
				TestConstants.TotalGamesPlayed,
				TestConstants.AverageGuesses
			);
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim());
		}

		[TestMethod]
		public void DisplayHighScoreListHeader_ValidInputs_ShouldFormatHeaderCorrectly()
		{
			int leftPadding = (TestConstants.TotalWidth - HighScoreHeaders.HighScoreHeader.Length) / 2;
			string separatorLine = new('-', TestConstants.TotalWidth);
			string headerRowFormat = string.Format(
				"{0,-" + FormattingConstants.RankColumnWidth + "} {1,-" + TestConstants.MaxUserNameLength + "} {2," + FormattingConstants.GamesPlayedColumnWidth + "} {3," + FormattingConstants.AverageGuessesColumnWidth + "}",
				HighScoreHeaders.RankHeader,
				HighScoreHeaders.PlayerHeader,
				HighScoreHeaders.GamesHeader,
				HighScoreHeaders.AverageGuessesHeader
			);
			var expectedHeaderOutput = $"{new string(' ', leftPadding)}{HighScoreHeaders.HighScoreHeader}\n" +
									   $"{separatorLine}\n" +
									   $"{headerRowFormat}\n" +
									   $"{separatorLine}";
			_consoleUI.DisplayHighScoreListHeader(TestConstants.MaxUserNameLength, TestConstants.TotalWidth);
			Assert.AreEqual(expectedHeaderOutput, _consoleOutput.ToString().TrimEnd());
		}

		[TestMethod]
		public void AskToContinue_ValidYesInput_ReturnsTrue()
		{
			var mockConsoleUI = new Mock<IConsoleUI>();
			mockConsoleUI.Setup(ui => ui.AskToContinue()).Returns(true);
			var result = mockConsoleUI.Object.AskToContinue();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void AskToContinue_ValidNoInput_ReturnsFalse()
		{
			var mockConsoleUI = new Mock<IConsoleUI>();
			mockConsoleUI.Setup(ui => ui.AskToContinue()).Returns(false);
			var result = mockConsoleUI.Object.AskToContinue();
			Assert.IsFalse(result);
		}

		[TestCleanup]
		public void Cleanup()
		{
			Console.SetOut(_originalConsoleOut);
			Console.SetIn(_originalConsoleIn);
			_consoleOutput.Dispose();
		}

		// Create a test-specific ConsoleUIBase instance
		private ConsoleUIBase CreateTestConsoleUI()
		{
			return new TestConsoleUI(_mockValidation.Object, _mockHighScoreManager.Object);
		}

		// Derived class for testing abstract methods
		private class TestConsoleUI(IValidation validation, IHighScoreManager highScoreManager) : ConsoleUIBase(validation, highScoreManager)
		{
			public override string GetWelcomeMessageFormat(GameTypes gameType) => "Welcome {0}!";

			public override string GetGoodbyeMessageFormat(GameTypes gameType) => "Goodbye {0}!";

			public override void DisplayInvalidInputMessage() => Console.WriteLine("Invalid input. Please try again.");
		}
	}
}