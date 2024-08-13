using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.ConstantsLibrary.Constants;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Mocks;
using Laboration.PlayerData.Interfaces;
using Laboration.Validation.Interfaces;
using Library.ConstantsLibrary.Constants;
using Moq;

namespace Laboration.UnitTests.ConsoleUI
{
	[TestClass]
	public class BullsAndCowsConsoleUITests
	{
		private readonly Mock<IValidation> _mockValidation = new();
		private readonly Mock<IHighScoreManager> _mockHighScoreManager = new();
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private BullsAndCowsConsoleUI _consoleUI = null!;
		private readonly StringWriter _consoleOutput = new();
		private readonly TextWriter _originalConsoleOut = Console.Out;
		private readonly TextReader _originalConsoleIn = Console.In;

		[TestInitialize]
		public void Setup()
		{
			_consoleUI = new BullsAndCowsConsoleUI(_mockValidation.Object, _mockHighScoreManager.Object);
			Console.SetOut(_consoleOutput);
		}

		[TestMethod]
		public void GetUserName_ValidUserName_ReturnsUserName()
		{
			// Arrange
			_mockConsoleUI.Setup(ui => ui.GetUserName()).Returns(TestConstants.UserName);

			// Act
			string userName = _mockConsoleUI.Object.GetUserName();

			// Assert
			Assert.AreEqual(TestConstants.UserName, userName, "The user name returned should be 'TestUser'.");
		}

		[TestMethod]
		public void DisplayWelcomeMessage_ShouldPrintDetailedMessage_WhenNewGame()
		{
			// Arrange
			var expectedOutput = string.Format(
				MessageConstants.WelcomeMessageFormat,
				TestConstants.UserName
			);

			// Act
			_consoleUI.DisplayWelcomeMessage(TestConstants.UserName, true);

			// Assert
			string actualOutput = _consoleOutput.ToString().Trim();
			Assert.AreEqual(expectedOutput, actualOutput,
				"The welcome message printed to the console does not match the expected output. Verify the formatting and content.");
		}

		[TestMethod]
		public void DisplayWelcomeMessage_ShouldPrintBriefMessage_WhenReturningPlayer()
		{
			// Arrange
			var expectedOutput = string.Format(
				MessageConstants.WelcomeBackMessageFormat,
				TestConstants.UserName
			);

			// Act
			_consoleUI.DisplayWelcomeMessage(TestConstants.UserName, false);

			// Assert
			string actualOutput = _consoleOutput.ToString().Trim();
			Assert.AreEqual(expectedOutput, actualOutput,
				"The welcome back message printed to the console does not match the expected output. Verify the formatting and content.");
		}

		[TestMethod]
		public void DisplaySecretNumberForPractice_ShouldPrintSecretNumber()
		{
			// Arrange
			var expectedOutput = string.Format(
				MessageConstants.SecretNumberPracticeMessage,
				TestConstants.SecretNumber
			);

			// Act
			_consoleUI.DisplaySecretNumberForPractice(TestConstants.SecretNumber);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The secret number message should match the expected output.");
		}

		[TestMethod]
		public void DisplayCorrectMessage_SingleGuess_DisplaysMessage()
		{
			// Arrange
			var expectedOutput = string.Format(
				MessageConstants.CorrectGuessMessageFormat,
				TestConstants.SecretNumber,
				TestConstants.SingleGuess,
				MessageConstants.GuessSingular
			);

			// Act
			_consoleUI.DisplayCorrectMessage(TestConstants.SecretNumber, TestConstants.SingleGuess);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The message should match the expected output for a single guess.");
		}

		[TestMethod]
		public void DisplayCorrectMessage_MultipleGuesses_DisplaysMessage()
		{
			// Arrange
			var expectedOutput = string.Format(
				MessageConstants.CorrectGuessMessageFormat,
				TestConstants.SecretNumber,
				TestConstants.MultipleGuesses,
				MessageConstants.GuessPlural
			);

			// Act
			_consoleUI.DisplayCorrectMessage(TestConstants.SecretNumber, TestConstants.MultipleGuesses);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The message should match the expected output for multiple guesses.");
		}

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
			_mockHighScoreManager.Verify(
				m => m.ReadHighScoreResultsFromFile(),
				Times.Once,
				"ReadHighScoreResultsFromFile should be called once to retrieve the high score list."
			);

			_mockHighScoreManager.Verify(
				m => m.SortHighScoreList(expectedResults),
				Times.Once,
				"SortHighScoreList should be called once with the expected results to sort the high score list."
			);
		}

		[TestMethod]
		public void DisplayRank_ShouldFormatRankCorrectly()
		{
			// Arrange
			var expectedOutput = string.Format(
				"{0,-" + FormattingConstants.RankColumnWidth + "}",
				TestConstants.Rank
			);

			// Act
			_consoleUI.DisplayRank(int.Parse(TestConstants.Rank));

			// Assert
			var actualOutput = _consoleOutput.ToString();
			Assert.AreEqual(expectedOutput, actualOutput, "The rank output does not match the expected format.");
		}

		[TestMethod]
		public void DisplayPlayerData_ShouldPrintPlayerDataWithCorrectFormatting()
		{
			// Arrange
			var player = MockPlayerData.CreateMock();

			// Act
			_consoleUI.DisplayPlayerData(player, true, TestConstants.MaxUserNameLength);

			var output = _consoleOutput.ToString().Trim();

			// TODO: String interpolation
			var expectedOutput = string.Format(
				"{0,-" + TestConstants.MaxUserNameLength + "} {1," + FormattingConstants.GamesPlayedColumnWidth + "} {2," + FormattingConstants.AverageGuessesColumnWidth + "}",
				TestConstants.UserName,
				TestConstants.TotalGamesPlayed,
				TestConstants.AverageGuesses
			);

			// Assert
			Assert.AreEqual(expectedOutput, output, "Player data should be printed with correct formatting.");
		}

		[TestMethod]
		public void DisplayHighScoreListHeader_ValidInputs_ShouldFormatHeaderCorrectly()
		{
			// Arrange
			int leftPadding = (TestConstants.TotalWidth - DisplayConstants.HighScoreHeader.Length) / 2;

			string SeparatorLine = new('-', TestConstants.TotalWidth);

			string HeaderRowFormat = string.Format(
				"{0,-" + FormattingConstants.RankColumnWidth + "} {1,-" + TestConstants.MaxUserNameLength + "} {2," + FormattingConstants.GamesPlayedColumnWidth + "} {3," + FormattingConstants.AverageGuessesColumnWidth + "}",
				MessageConstants.RankHeader,
				MessageConstants.PlayerHeader,
				MessageConstants.GamesHeader,
				MessageConstants.AverageGuessesHeader
			);

			var expectedHeaderOutput = $"{new string(' ', leftPadding)}{DisplayConstants.HighScoreHeader}\n" +
									   $"{SeparatorLine}\n" +
									   $"{HeaderRowFormat}\n" +
									   $"{SeparatorLine}";

			// Act
			_consoleUI.DisplayHighScoreListHeader(TestConstants.MaxUserNameLength, TestConstants.TotalWidth);

			// Assert
			var actualOutput = _consoleOutput.ToString().TrimEnd();
			Assert.AreEqual(expectedHeaderOutput, actualOutput, "The high score list header should match the expected format.");
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