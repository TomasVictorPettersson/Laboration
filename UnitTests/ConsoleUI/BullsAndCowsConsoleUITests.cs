using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.PlayerData.Interfaces;
using Laboration.UnitTests.Mocks;
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

		private Mock<IValidation> _mockValidation = new();
		private Mock<IHighScoreManager> _mockHighScoreManager = new();
		private BullsAndCowsConsoleUI _consoleUI = new(null!, null!);
		private StringWriter _consoleOutput = new();
		private TextWriter _originalConsoleOut = Console.Out;
		private TextReader _originalConsoleIn = Console.In;
		private Mock<IConsoleUI> _mockConsoleUI = new();

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
			_mockConsoleUI.Setup(ui => ui.GetUserName()).Returns(UserName);

			// Act
			string userName = _mockConsoleUI.Object.GetUserName();

			// Assert
			Assert.AreEqual(UserName, userName, "The user name returned should be 'TestUser'.");
		}

		[TestMethod]
		public void DisplayCorrectMessage_ValidData_DisplaysMessage()
		{
			// Arrange
			const int numberOfGuesses = 5;
			var expectedOutput = $"Correct! The secret number was: {SecretNumber}\nIt took you {numberOfGuesses} guesses.";

			// Act
			_consoleUI.DisplayCorrectMessage(SecretNumber, numberOfGuesses);

			// Assert

			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The message should match the expected output.");
		}

		[TestMethod]
		public void DisplayWelcomeMessage_ShouldPrintWelcomeMessage()
		{
			// Arrange
			const string expectedOutput =
				$"Welcome {UserName} to Bulls and Cows!\n" +
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
			_consoleUI.DisplayWelcomeMessage(UserName);

			// Assert
			string actualOutput = consoleOutput.ToString().Trim();
			Assert.AreEqual(expectedOutput, actualOutput,
				"The welcome message printed to the console does not match the expected output. Verify the formatting and content of the welcome message.");
		}

		[TestMethod]
		public void DisplaySecretNumberForPractice_ShouldPrintSecretNumber()
		{
			// Arrange
			var expectedOutput = $"For practice, number is: {SecretNumber}";
			// Act
			_consoleUI.DisplaySecretNumberForPractice(SecretNumber);

			// Assert
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The secret number message should match the expected output.");
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

		[TestMethod]
		public void DisplayHighScoreList_ShouldDisplayHighScoreList()
		{
			// Arrange
			var mockHighScoreManager = new Mock<IHighScoreManager>();
			var consoleUI = new BullsAndCowsConsoleUI(null!, mockHighScoreManager.Object);

			var expectedResults = new List<IPlayerData>
				{
					MockPlayerData.CreateMock()
				};

			mockHighScoreManager.Setup(m => m.ReadHighScoreResultsFromFile()).Returns(expectedResults);

			// Act
			consoleUI.DisplayHighScoreList("TestUser");

			// Assert
			mockHighScoreManager.Verify(m => m.ReadHighScoreResultsFromFile(), Times.Once);
			mockHighScoreManager.Verify(m => m.SortHighScoreList(expectedResults), Times.Once);
		}

		[TestMethod]
		public void PrintHighScoreResults_ShouldPrintPlayerDataWithCorrectFormatting()
		{
			// Arrange
			var results = new List<IPlayerData>
			{
				MockPlayerData.CreateMock()
			};

			// Act
			_consoleUI.PrintHighScoreResults(results, UserName, MaxUserNameLength);

			var output = _consoleOutput.ToString().Trim();

			// Construct the expected output
			var expectedOutput = $"{Rank,-RankColumnWidth}{UserName,-MaxUserNameLength} {TotalGamesPlayed,GamesPlayedColumnWidth} {AverageGuesses,AverageGuessesColumnWidth}";

			// Assert
			Assert.AreEqual(expectedOutput, output, "Player data should be printed with correct formatting.");
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
			const string header = "=== High Score List ===";
			int leftPadding = (totalWidth - header.Length) / 2;

			var expectedHeaderOutput = $@"
			{new string(' ', leftPadding)}{header}
			{"Rank",-RankColumnWidth} {"Player",-MaxUserNameLength} {"Games",-GamesPlayedColumnWidth} {"Average Guesses",-AverageGuessesColumnWidth}
			{new string('-', totalWidth)}";

			// Act
			_consoleUI.DisplayHighScoreListHeader(MaxUserNameLength, totalWidth);

			// Assert
			var actualOutput = _consoleOutput.ToString().TrimEnd();
			Assert.AreEqual(expectedHeaderOutput, actualOutput, "The high score list header should match the expected format.");
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
				_consoleOutput?.Dispose();
			}
		}
	}
}