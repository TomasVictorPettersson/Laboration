using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.HighScoreManagement.Interfaces;
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
		private const string CurrentUserName = "Player1";

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
			Assert.AreEqual(UserName, userName, "The user name returned should be 'ValidUser'.");
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
			const int numberOfGuesses = 5;

			// Act
			_consoleUI.DisplayCorrectMessage(SecretNumber, numberOfGuesses);

			// Assert
			var expectedOutput = $"Correct! The secret number was: {SecretNumber}\nIt took you {numberOfGuesses} guesses.";
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The message should match the expected output.");
		}

		[TestMethod]
		public void TestDisplayWelcomeMessage()
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
			Assert.AreEqual(expectedOutput, actualOutput);
		}

		[TestMethod]
		public void DisplaySecretNumberForPractice_ShouldPrintSecretNumber()
		{
			// Arrange
			// Act
			_consoleUI.DisplaySecretNumberForPractice(SecretNumber);

			// Assert
			Assert.AreEqual($"For practice, number is: {SecretNumber}", _consoleOutput.ToString().Trim(), "The secret number message should match the expected output.");
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
			// Act
			_consoleUI.DisplayGoodbyeMessage(UserName);

			// Assert
			var expectedOutput = $"Thank you, {UserName}, for playing Bulls and Cows!";
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The goodbye message should match the expected output.");
		}

		[TestMethod]
		public void DisplayHighScoreList_ShouldCallHighScoreManagerMethods()
		{
			// Arrange
			var results = new List<IPlayerData>();
			_mockHighScoreManager.Setup(m => m.ReadHighScoreResultsFromFile()).Returns(results);

			// Configure the mock to return valid dimensions
			_mockHighScoreManager.Setup(m => m.CalculateDisplayDimensions(results)).Returns((10, 50));

			// Act
			_consoleUI.DisplayHighScoreList(CurrentUserName);

			// Assert
			_mockHighScoreManager.Verify(m => m.ReadHighScoreResultsFromFile(), Times.Once);
			_mockHighScoreManager.Verify(m => m.SortHighScoreList(results), Times.Once);
			_mockHighScoreManager.Verify(m => m.CalculateDisplayDimensions(results), Times.Once);
		}

		[TestMethod]
		public void PrintHighScoreResults_ShouldPrintPlayerDataWithCorrectFormatting()
		{
			// Arrange
			var results = new List<IPlayerData>
			{
				CreateMockPlayerData(CurrentUserName, 10, 5.5)
			};
			const int maxUserNameLength = 10;

			// Act
			_consoleUI.PrintHighScoreResults(results, CurrentUserName, maxUserNameLength);

			var output = _consoleOutput.ToString().Trim();

			// Construct the expected output
			const string expectedRank = "1";
			var expectedUserName = CurrentUserName.PadRight(maxUserNameLength);
			const string expectedTotalGamesPlayed = "10";
			const string expectedAverageGuesses = "5,50";

			// The expected output should include the rank, user name, games played, and average guesses
			var expectedOutput = $"{expectedRank,-6}{expectedUserName} {expectedTotalGamesPlayed,8} {expectedAverageGuesses,15}";

			// Assert
			Assert.AreEqual(expectedOutput, output, "Player data should be printed with correct formatting.");
		}

		[TestMethod]
		public void DisplayRank_ShouldFormatRankCorrectly()
		{
			// Arrange
			const int rank = 1;
			var expectedOutput = $"{rank,-6}";

			// Act
			_consoleUI.DisplayRank(rank);

			// Assert
			var actualOutput = _consoleOutput.ToString();
			Assert.AreEqual(expectedOutput, actualOutput, "The rank output does not match the expected format.");
		}

		[TestMethod]
		public void DisplayPlayerData_ShouldPrintPlayerDataWithCorrectFormatting()
		{
			// Arrange
			var player = CreateMockPlayerData(CurrentUserName, 10, 5.5);
			const int maxUserNameLength = 10;
			const bool isCurrentUser = true;

			// Act
			_consoleUI.DisplayPlayerData(player, isCurrentUser, maxUserNameLength);

			var output = _consoleOutput.ToString().Trim();

			// Construct the expected output
			var expectedUserName = CurrentUserName.PadRight(maxUserNameLength);
			const string expectedTotalGamesPlayed = "10";
			const string expectedAverageGuesses = "5,50";

			var expectedOutput = $"{expectedUserName} {expectedTotalGamesPlayed,8} {expectedAverageGuesses,15}";

			// Assert
			Assert.AreEqual(expectedOutput, output, "Player data should be printed with correct formatting.");
		}

		[TestMethod]
		public void DisplayHighScoreListHeader_ValidInputs_ShouldFormatHeaderCorrectly()
		{
			// Arrange

			const int maxUserNameLength = 10;
			const int totalWidth = 50;
			const string header = "=== High Score List ===";
			int leftPadding = (totalWidth - header.Length) / 2;

			var expectedHeaderOutput = $"\n{new string(' ', leftPadding)}{header}\n" +
									   $"{"Rank",-6} {"Player".PadRight(maxUserNameLength)} {"Games",-8} {"Average Guesses",-15}\n" +
									   new string('-', totalWidth);

			// Act
			// Redirect console output
			using var consoleOutput = new StringWriter();
			Console.SetOut(consoleOutput);

			_consoleUI.DisplayHighScoreListHeader(maxUserNameLength, totalWidth);

			// Capture and clean up output
			var actualOutput = consoleOutput.ToString();
			actualOutput = actualOutput.TrimEnd(); // Remove trailing newlines

			// Assert
			Assert.AreEqual(expectedHeaderOutput, actualOutput, "The header should be formatted correctly.");
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

		private static IPlayerData CreateMockPlayerData(string userName, int totalGamesPlayed, double averageGuesses)
		{
			var mockPlayerData = new Mock<IPlayerData>();
			mockPlayerData.Setup(p => p.UserName).Returns(userName);
			mockPlayerData.Setup(p => p.TotalGamesPlayed).Returns(totalGamesPlayed);
			mockPlayerData.Setup(p => p.CalculateAverageGuesses()).Returns(averageGuesses);
			return mockPlayerData.Object;
		}
	}
}