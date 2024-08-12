using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Mocks;
using Laboration.PlayerData.Interfaces;
using Laboration.Validation.Interfaces;
using Moq;

namespace Laboration.UnitTests.ConsoleUI
{
	[TestClass]
	public class BullsAndCowsConsoleUITests
	{
		private const string UserName = "TestUser";
		private const string SecretNumber = "1234";
		private const string TotalGamesPlayed = "10";
		private const string AverageGuesses = "5,50";
		private const int MaxUserNameLength = 10;
		private const string Rank = "1";
		private const int RankColumnWidth = 6;
		private const int GamesPlayedColumnWidth = 8;
		private const int AverageGuessesColumnWidth = 15;
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
			_mockConsoleUI.Setup(ui => ui.GetUserName()).Returns(UserName);

			// Act
			string userName = _mockConsoleUI.Object.GetUserName();

			// Assert
			Assert.AreEqual(UserName, userName, "The user name returned should be 'TestUser'.");
		}

		[TestMethod]
		public void DisplayWelcomeMessage_ShouldPrintWelcomeMessage()
		{
			// Arrange
			const string expectedOutput =
				$"Welcome, {UserName}, to Bulls and Cows!\n" +
				"\nThe objective of the game is to guess a 4-digit number.\n" +
				"Each digit in the 4-digit number will only appear once.\n" +
				"You can only use digits from 0 to 9.\n" +
				"\nFor each guess, you will receive feedback in the form 'BBBB,CCCC', where:\n" +
				"- 'BBBB' represents the number of bulls (correct digits in correct positions).\n" +
				"- 'CCCC' represents the number of cows (correct digits in wrong positions).\n" +
				"If the feedback is 'No matches', it means none of the digits in your guess are present in the 4-digit number.";

			// Act
			_consoleUI.DisplayWelcomeMessage(UserName);

			// Assert
			string actualOutput = _consoleOutput.ToString().Trim();
			Assert.AreEqual(expectedOutput, actualOutput,
				"The welcome message printed to the console does not match the expected output. Verify the formatting and content of the welcome message.");
		}

		[TestMethod]
		public void DisplaySecretNumberForPractice_ShouldPrintSecretNumber()
		{
			// Arrange
			var expectedOutput = $"For practice mode, the secret number is: {SecretNumber}";

			// Act
			_consoleUI.DisplaySecretNumberForPractice(SecretNumber);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The secret number message should match the expected output.");
		}

		[TestMethod]
		public void DisplayCorrectMessage_SingleGuess_DisplaysMessage()
		{
			// Arrange
			const int numberOfGuesses = 1;

			var expectedOutput = $"Correct! The secret number was: {SecretNumber}\nIt took you {numberOfGuesses} guess.";

			// Act
			_consoleUI.DisplayCorrectMessage(SecretNumber, numberOfGuesses);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The message should match the expected output for a single guess.");
		}

		[TestMethod]
		public void DisplayCorrectMessage_MultipleGuesses_DisplaysMessage()
		{
			// Arrange
			const int numberOfGuesses = 5;

			var expectedOutput = $"Correct! The secret number was: {SecretNumber}\nIt took you {numberOfGuesses} guesses.";

			// Act
			_consoleUI.DisplayCorrectMessage(SecretNumber, numberOfGuesses);

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
			_consoleUI.DisplayHighScoreList(UserName);

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
			var expectedOutput = $"{Rank,-RankColumnWidth}";

			// Act
			_consoleUI.DisplayRank(int.Parse(Rank));

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
			_consoleUI.DisplayPlayerData(player, true, MaxUserNameLength);

			var output = _consoleOutput.ToString().Trim();

			var expectedOutput = $"{UserName,-MaxUserNameLength} {TotalGamesPlayed,GamesPlayedColumnWidth} {AverageGuesses,AverageGuessesColumnWidth}";

			// Assert
			Assert.AreEqual(expectedOutput, output, "Player data should be printed with correct formatting.");
		}

		[TestMethod]
		public void DisplayHighScoreListHeader_ValidInputs_ShouldFormatHeaderCorrectly()
		{
			// Arrange
			const int totalWidth = 50;
			const int maxUserNameLength = 20;

			const string header = "=== HIGH SCORE LIST ===";
			int leftPadding = (totalWidth - header.Length) / 2;

			string SeparatorLine = new('-', totalWidth);

			string HeaderRowFormat = $"{"Rank",-RankColumnWidth} {"Player",-maxUserNameLength} {"   Games",-GamesPlayedColumnWidth} {"   Avg. Guesses",-AverageGuessesColumnWidth}";

			var expectedHeaderOutput = $"{new string(' ', leftPadding)}{header}\n" +
									   $"{SeparatorLine}\n" +
									   $"{HeaderRowFormat}\n" +
									   $"{SeparatorLine}";

			// Act
			_consoleUI.DisplayHighScoreListHeader(maxUserNameLength, totalWidth);

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

		[TestMethod]
		public void DisplayGoodbyeMessage_ShouldPrintGoodbyeMessage()
		{
			// Arrange
			var expectedOutput = $"Thank you, {UserName}, for playing Bulls and Cows!";

			// Act
			_consoleUI.DisplayGoodbyeMessage(UserName);

			// Assert
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