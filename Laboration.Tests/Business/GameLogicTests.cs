using Laboration.Business.Classes;
using Laboration.Common.Interfaces;
using Laboration.UI.Interfaces;
using Moq;

namespace Laboration.Tests.Business
{
	[TestClass]
	public class GameLogicTests
	{
		private Mock<IHighScoreManager> _highScoreManagerMock;
		private Mock<IUserInterface> _userInterfaceMock;
		private GameLogic _gameLogic;

		[TestInitialize]
		public void Initialize()
		{
			_highScoreManagerMock = new Mock<IHighScoreManager>();
			_userInterfaceMock = new Mock<IUserInterface>();
			_gameLogic = new GameLogic(_highScoreManagerMock.Object, _userInterfaceMock.Object);
		}

		[TestMethod]
		public void DisplayWelcomeMessage_WritesCorrectMessagesToConsole()
		{
			// Arrange
			const string userName = "JohnDoe";
			StringWriter sw = new();
			Console.SetOut(sw);

			// Act
			_gameLogic.DisplayWelcomeMessage(userName);
			string consoleOutput = sw.ToString();

			// Assert
			StringAssert.Contains(consoleOutput, $"Welcome {userName} to Bulls and Cows!");
			StringAssert.Contains(consoleOutput, "The objective of the game is to guess a 4-digit number.");
			StringAssert.Contains(consoleOutput, "For each guess, you will receive feedback in the form of 'BBBB,CCCC',");
			StringAssert.Contains(consoleOutput, "where 'BBBB' represents the number of bulls (correct digits in the correct positions),");
			StringAssert.Contains(consoleOutput, "and 'CCCC' represents the number of cows (correct digits in the wrong positions).\n");
		}

		[TestMethod]
		public void MakeSecretNumber_GeneratesUnique4DigitNumber()
		{
			// Arrange
			string secretNumber1 = _gameLogic.MakeSecretNumber();
			string secretNumber2 = _gameLogic.MakeSecretNumber();

			// Act and Assert
			Assert.AreNotEqual(secretNumber1, secretNumber2);
			Assert.IsTrue(secretNumber1.Length == 4 && secretNumber2.Length == 4);
			Assert.IsTrue(int.TryParse(secretNumber1, out _) && int.TryParse(secretNumber2, out _));
		}

		[TestMethod]
		public void ProcessGuess_ValidInput_GuessEqualsSecretNumber_IncrementsGuessCounter()
		{
			// Arrange
			int initialGuessCount = 0;
			string secretNumber = "1234";
			string guess = "1234";

			using var sw = new StringWriter();
			using var sr = new StringReader(guess);
			Console.SetOut(sw);
			Console.SetIn(sr);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(1, initialGuessCount);
			var output = sw.ToString();
			StringAssert.Contains(output, "BBBB,");
		}

		[TestMethod]
		public void ProcessGuess_ValidInput_GuessNotEqualsSecretNumber_IncrementsGuessCounter()
		{
			// Arrange
			int initialGuessCount = 0;
			string secretNumber = "1234";
			string guess = "5678";

			using var sw = new StringWriter();
			using var sr = new StringReader(guess);
			Console.SetOut(sw);
			Console.SetIn(sr);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(1, initialGuessCount);
			var output = sw.ToString();
			StringAssert.Contains(output, ",");
		}

		[TestMethod]
		public void ProcessGuess_InvalidInput_DoesNotIncrementGuessCounter_AndDisplaysErrorMessage()
		{
			// Arrange
			int initialGuessCount = 0;
			string secretNumber = "1234";
			string invalidGuess = "123";

			using var sw = new StringWriter();
			using var sr = new StringReader(invalidGuess);
			Console.SetOut(sw);
			Console.SetIn(sr);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(0, initialGuessCount);
			var output = sw.ToString();
			StringAssert.Contains(output, "Invalid input. Please enter a 4-digit number.");
		}

		[TestMethod]
		public void GenerateBullsAndCowsFeedback_CorrectGuess_ReturnsBBBB()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "1234";

			// Act
			string feedback = _gameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual("BBBB,", feedback);
		}

		[TestMethod]
		public void GenerateBullsAndCowsFeedback_IncorrectGuess_ReturnsCorrectBullsAndCows()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "1568";

			// Act
			string feedback = _gameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreNotEqual("BBBB,", feedback);
			Assert.IsTrue(feedback.Contains("B") || feedback.Contains("C"));
		}

		[TestMethod]
		public void GenerateBullsAndCowsFeedback_CorrectCows_ReturnsCorrectCCCC()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "4321";

			// Act
			string feedback = _gameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual(",CCCC", feedback);
		}

		[TestMethod]
		public void GenerateBullsAndCowsFeedback_PartialMatch_ReturnsMixedBullsAndCows()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "1243";

			// Act
			string feedback = _gameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual("BB,CC", feedback);
		}

		[TestMethod]
		public void GenerateBullsAndCowsFeedback_EdgeCase_RepeatingDigits()
		{
			// Arrange
			const string secretNumber = "1122";
			const string guess = "2211";

			// Act
			string feedback = _gameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual(",CCCC", feedback);
		}

		[TestCleanup]
		public void Cleanup()
		{
			Console.SetOut(Console.Out);
			Console.SetIn(new StreamReader(Console.OpenStandardInput()));
		}
	}
}