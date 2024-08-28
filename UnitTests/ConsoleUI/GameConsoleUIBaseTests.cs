using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.GameResources.Constants;
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

		// Test case to ensure the secret number for practice is displayed correctly.
		[TestMethod]
		public void DisplaySecretNumberForPractice_ShouldPrintSecretNumber()
		{
			// Arrange
			var expectedOutput = string.Format(GameMessages.SecretNumberPracticeMessage, TestConstants.SecretNumber);

			// Act
			_consoleUI.DisplaySecretNumberForPractice(TestConstants.SecretNumber);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The secret number for practice was not displayed correctly.");
		}

		// Test case to ensure no matches feedback is displayed correctly.
		[TestMethod]
		public void DisplayGuessFeedback_NoMatches_ShouldDisplayNoMatchesMessage()
		{
			// Arrange
			var expectedOutput = $"{GameMessages.FeedbackPrefix}{GameMessages.NoMatchesFoundMessage}";

			// Act
			_consoleUI.DisplayGuessFeedback(",");

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The feedback for no matches was not displayed correctly.");
		}

		// Test case to ensure guess feedback is displayed correctly when there are matches.
		[TestMethod]
		public void DisplayGuessFeedback_MatchesFound_ShouldDisplayFeedback()
		{
			// Arrange
			var expectedOutput = $"{GameMessages.FeedbackPrefix}{TestConstants.FeedbackBBBB}";

			// Act
			_consoleUI.DisplayGuessFeedback(TestConstants.FeedbackBBBB);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The feedback for matches found was not displayed correctly.");
		}

		// Test case to ensure the correct message is displayed for a single guess.
		[TestMethod]
		public void DisplayCorrectMessage_SingleGuess_DisplaysMessage()
		{
			// Arrange
			var expectedOutput = string.Format(GameMessages.CorrectGuessMessageFormat, TestConstants.SecretNumber, TestConstants.SingleGuess, Plurals.GuessSingular);

			// Act
			_consoleUI.DisplayCorrectMessage(TestConstants.SecretNumber, TestConstants.SingleGuess);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The correct guess message for a single guess is not correct.");
		}

		// Test case to ensure the correct message is displayed for multiple guesses.
		[TestMethod]
		public void DisplayCorrectMessage_MultipleGuesses_DisplaysMessage()
		{
			// Arrange
			var expectedOutput = string.Format(GameMessages.CorrectGuessMessageFormat, TestConstants.SecretNumber, TestConstants.MultipleGuesses, Plurals.GuessPlural);

			// Act
			_consoleUI.DisplayCorrectMessage(TestConstants.SecretNumber, TestConstants.MultipleGuesses);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The correct guess message for multiple guesses is not correct.");
		}

		// Test case to ensure the high score list is displayed correctly.
		[TestMethod]
		public void DisplayHighScoreList_ShouldDisplayHighScoreList()
		{
			// Arrange
			var expectedResults = new List<IPlayerData>
			{
				MockPlayerData.CreateMock()
			};
			_mockHighScoreManager.Setup(m => m.ReadHighScoreResultsFromFile()).Returns(expectedResults);

			// Act
			_consoleUI.DisplayHighScoreList(TestConstants.UserName);

			// Assert
			_mockHighScoreManager.Verify(m => m.ReadHighScoreResultsFromFile(), Times.Once, "ReadHighScoreResultsFromFile was not called.");
			_mockHighScoreManager.Verify(m => m.SortHighScoreList(expectedResults), Times.Once, "SortHighScoreList was not called.");
		}

		// Test case to ensure rank formatting is correct.
		[TestMethod]
		public void DisplayRank_ShouldFormatRankCorrectly()
		{
			// Arrange
			var expectedOutput = string.Format("{0,-" + FormattingConstants.RankColumnWidth + "}", TestConstants.Rank);

			// Act
			_consoleUI.DisplayRank(int.Parse(TestConstants.Rank));

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString(), "The rank display format is not correct.");
		}

		// Test case to ensure player data is displayed with correct formatting.
		[TestMethod]
		public void DisplayPlayerData_ShouldPrintPlayerDataWithCorrectFormatting()
		{
			// Arrange
			var player = MockPlayerData.CreateMock();
			var expectedOutput = string.Format(
				"{0,-" + TestConstants.MaxUserNameLength + "} {1," + FormattingConstants.GamesPlayedColumnWidth + "} {2," + FormattingConstants.AverageGuessesColumnWidth + "}",
				TestConstants.UserName,
				TestConstants.TotalGamesPlayed,
				TestConstants.AverageGuesses
			);

			// Act
			_consoleUI.DisplayPlayerData(player, true, TestConstants.MaxUserNameLength);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The player data display format is not correct.");
		}

		// Test case to ensure the high score list header is formatted correctly.
		[TestMethod]
		public void DisplayHighScoreListHeader_ValidInputs_ShouldFormatHeaderCorrectly()
		{
			// Arrange
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

			// Act
			_consoleUI.DisplayHighScoreListHeader(TestConstants.MaxUserNameLength, TestConstants.TotalWidth);

			// Assert
			Assert.AreEqual(expectedHeaderOutput, _consoleOutput.ToString().TrimEnd(), "The high score list header format is not correct.");
		}

		// Test case to ensure AskToContinue returns true for a valid 'Yes' input.
		[TestMethod]
		public void AskToContinue_ValidYesInput_ReturnsTrue()
		{
			// Arrange
			var mockConsoleUI = new Mock<IConsoleUI>();
			mockConsoleUI.Setup(ui => ui.AskToContinue()).Returns(true);

			// Act
			var result = mockConsoleUI.Object.AskToContinue();

			// Assert
			Assert.IsTrue(result, "AskToContinue did not return true for 'Yes' input.");
		}

		// Test case to ensure AskToContinue returns false for a valid 'No' input.
		[TestMethod]
		public void AskToContinue_ValidNoInput_ReturnsFalse()
		{
			// Arrange
			var mockConsoleUI = new Mock<IConsoleUI>();
			mockConsoleUI.Setup(ui => ui.AskToContinue()).Returns(false);

			// Act
			var result = mockConsoleUI.Object.AskToContinue();

			// Assert
			Assert.IsFalse(result, "AskToContinue did not return false for 'No' input.");
		}

		// Cleanup after each test method is run.
		[TestCleanup]
		public void Cleanup()
		{
			// Arrange
			Console.SetOut(_originalConsoleOut);
			Console.SetIn(_originalConsoleIn);

			// Act
			_consoleOutput.Dispose();
		}
	}
}