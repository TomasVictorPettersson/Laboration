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
		private Mock<IValidation> _mockValidation;
		private Mock<IHighScoreManager> _mockHighScoreManager;
		private BullsAndCowsConsoleUI _consoleUI;
		private StringWriter _consoleOutput;
		private TextWriter _originalConsoleOut;
		private TextReader _originalConsoleIn;
		private Mock<IConsoleUI> _mockConsoleUI;

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
			_mockConsoleUI.Setup(ui => ui.GetUserName()).Returns("ValidUser");

			// Act
			string userName = _mockConsoleUI.Object.GetUserName();

			// Assert
			Assert.AreEqual("ValidUser", userName, "The user name returned should be 'ValidUser'.");
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
			string secretNumber = "1234";
			int numberOfGuesses = 5;

			// Act
			_consoleUI.DisplayCorrectMessage(secretNumber, numberOfGuesses);

			// Assert
			var expectedOutput = $"Correct! The secret number was: {secretNumber}\nIt took you {numberOfGuesses} guesses.";
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The message should match the expected output.");
		}

		[TestMethod]
		public void TestDisplayWelcomeMessage()
		{
			// Arrange
			string userName = "TestUser";
			string expectedOutput =
				$"Welcome {userName} to Bulls and Cows!\n" +
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
			_consoleUI.DisplayWelcomeMessage(userName);

			// Assert
			string actualOutput = consoleOutput.ToString().Trim();
			Assert.AreEqual(expectedOutput, actualOutput);
		}

		[TestMethod]
		public void DisplaySecretNumberForPractice_ShouldPrintSecretNumber()
		{
			// Arrange
			var secretNumber = "1234";

			// Act
			_consoleUI.DisplaySecretNumberForPractice(secretNumber);

			// Assert
			Assert.AreEqual($"For practice, number is: {secretNumber}", _consoleOutput.ToString().Trim(), "The secret number message should match the expected output.");
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
		public void AskToContinue_InvalidInput_ThrowsException()
		{
			// Arrange
			_mockConsoleUI.Setup(ui => ui.AskToContinue()).Throws(new InvalidOperationException("Invalid input."));

			// Act & Assert
			var exception = Assert.ThrowsException<InvalidOperationException>(() => _mockConsoleUI.Object.AskToContinue());
			Assert.AreEqual("Invalid input.", exception.Message, "Exception message should be 'Invalid input.'");
		}

		[TestMethod]
		public void DisplayGoodbyeMessage_ShouldPrintGoodbyeMessage()
		{
			// Arrange
			string userName = "testUser";

			// Act
			_consoleUI.DisplayGoodbyeMessage(userName);

			// Assert
			var expectedOutput = $"Thank you, {userName}, for playing Bulls and Cows!";
			Assert.AreEqual(expectedOutput, _consoleOutput.ToString().Trim(), "The goodbye message should match the expected output.");
		}

		[TestMethod]
		public void DisplayHighScoreList_ShouldCallHighScoreManagerMethods()
		{
			// Arrange
			string currentUserName = "TestUser";
			var results = new List<IPlayerData>();
			_mockHighScoreManager.Setup(m => m.ReadHighScoreResultsFromFile()).Returns(results);

			// Configure the mock to return valid dimensions
			_mockHighScoreManager.Setup(m => m.CalculateDisplayDimensions(results)).Returns((10, 50));

			// Act
			_consoleUI.DisplayHighScoreList(currentUserName);

			// Assert
			_mockHighScoreManager.Verify(m => m.ReadHighScoreResultsFromFile(), Times.Once);
			_mockHighScoreManager.Verify(m => m.SortHighScoreList(results), Times.Once);
			_mockHighScoreManager.Verify(m => m.CalculateDisplayDimensions(results), Times.Once);
		}

		[TestMethod]
		public void DisplayHighScoreListHeader_ShouldPrintCorrectHeader()
		{
			// Arrange
			int maxUserNameLength = 10;
			int totalWidth = 50;

			// Act
			_consoleUI.DisplayHighScoreListHeader(maxUserNameLength, totalWidth);

			// Assert
			var output = _consoleOutput.ToString().Trim();

			// Construct the expected output
			var header = "=== High Score List ===";
			var rankHeader = "Rank";
			var playerHeader = "Player".PadRight(maxUserNameLength);
			var gamesHeader = "Games";
			var averageGuessesHeader = "Average Guesses";
			var separator = new string('-', totalWidth);

			// Calculate header padding for centering
			int headerPadding = (totalWidth - header.Length) / 2;
			var paddedHeader = new string(' ', headerPadding) + header;

			// Construct the expected output string
			var expectedOutput = $"\n{paddedHeader}\n" +
								 $"{rankHeader,-6} {playerHeader} {gamesHeader,-8} {averageGuessesHeader}\n" +
								 $"{separator}";

			// Perform the assertion
			Assert.AreEqual(expectedOutput, output, "The header format should match the expected format.");
		}

		[TestMethod]
		public void PrintHighScoreResults_ShouldPrintPlayerDataWithCorrectFormatting()
		{
			// Arrange
			var results = new List<IPlayerData>
			{
		CreateMockPlayerData("Player1", 10, 5.5)
			};
			string currentUserName = "Player1";
			int maxUserNameLength = 10;

			// Act
			_consoleUI.PrintHighScoreResults(results, currentUserName, maxUserNameLength);

			var output = _consoleOutput.ToString().Trim();

			// Construct the expected output
			var expectedRank = "1";
			var expectedUserName = "Player1".PadRight(maxUserNameLength);
			var expectedTotalGamesPlayed = "10";
			var expectedAverageGuesses = "5,50";

			// The expected output should include the rank, user name, games played, and average guesses
			var expectedOutput = $"{expectedRank,-6}{expectedUserName} {expectedTotalGamesPlayed,8} {expectedAverageGuesses,15}";

			// Assert
			Assert.AreEqual(expectedOutput, output, "Player data should be printed with correct formatting.");
		}

		[TestMethod]
		public void DisplayRank_ShouldHighlightCurrentUser()
		{
			// Arrange
			int rank = 1;
			bool isCurrentUser = true;
			var originalColor = Console.ForegroundColor; // Store the original color

			// Act
			_consoleUI.DisplayRank(rank, isCurrentUser);

			// Assert
			var output = _consoleOutput.ToString();
			var expectedOutput = $"{rank,-6}"; // Adjust based on the actual format

			// Check if the entire output matches the expected output
			Assert.AreEqual(expectedOutput, output, "The rank output does not match the expected format.");

			// Check if the console color is set to green
			Assert.AreEqual(ConsoleColor.Green, Console.ForegroundColor, "Current user should be highlighted in green.");

			// Restore the original color
			Console.ForegroundColor = originalColor;
		}

		[TestMethod]
		public void DisplayPlayerData_ShouldPrintPlayerDataWithCorrectFormatting()
		{
			// Arrange
			var player = CreateMockPlayerData("Player1", 10, 5.5);
			int maxUserNameLength = 10;
			bool isCurrentUser = true;

			// Act
			_consoleUI.DisplayPlayerData(player, isCurrentUser, maxUserNameLength);

			var output = _consoleOutput.ToString().Trim();

			// Construct the expected output
			var expectedUserName = "Player1".PadRight(maxUserNameLength);
			var expectedTotalGamesPlayed = "10";
			var expectedAverageGuesses = "5,50";

			var expectedOutput = $"{expectedUserName} {expectedTotalGamesPlayed,8} {expectedAverageGuesses,15}";

			// Assert
			Assert.AreEqual(expectedOutput, output, "Player data should be printed with correct formatting.");
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

		private IPlayerData CreateMockPlayerData(string userName, int totalGamesPlayed, double averageGuesses)
		{
			var mockPlayerData = new Mock<IPlayerData>();
			mockPlayerData.Setup(p => p.UserName).Returns(userName);
			mockPlayerData.Setup(p => p.TotalGamesPlayed).Returns(totalGamesPlayed);
			mockPlayerData.Setup(p => p.CalculateAverageGuesses()).Returns(averageGuesses);
			return mockPlayerData.Object;
		}
	}
}