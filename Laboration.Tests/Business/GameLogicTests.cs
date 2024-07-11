using Laboration.Business.Classes;
using Laboration.Common.Interfaces;
using Laboration.UI.Interfaces;
using Moq;

namespace Laboration.Tests.Business
{
	[TestClass]
	public class GameLogicTests
	{
		private readonly Mock<IHighScoreManager> _highScoreManagerMock = new();
		private readonly Mock<IUserInterface> _userInterfaceMock = new();
		private readonly GameLogic _gameLogic;

		public GameLogicTests()
		{
			GameConfig config = new() { MaxRetries = 3 };
			_gameLogic = new GameLogic(_highScoreManagerMock.Object, _userInterfaceMock.Object, config);
		}

		[TestMethod]
		public void WelcomeMessage_WritesCorrectMessagesToConsole()
		{
			// Arrange
			const string userName = "JohnDoe";
			StringWriter sw = new();
			Console.SetOut(sw);

			// Act
			_gameLogic.DisplayWelcomeMessage(userName);
			string consoleOutput = sw.ToString();

			// Assert
			Assert.IsTrue(consoleOutput.Contains($"Welcome {userName} to Bulls and Cows!"));
			Assert.IsTrue(consoleOutput.Contains("The objective is to guess a 4-digit number."));
			Assert.IsTrue(consoleOutput.Contains("Feedback: 'BBBB' for bulls (correct in position), 'CCCC' for cows (correct in wrong position).\n"));
		}

		[TestMethod]
		public void GenerateUnique4DigitNumber()
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
		public void ProcessGuess_GuessEqualsSecretNumber_IncrementsGuessCounter()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string guess = "1234";

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
		public void ProcessGuess_GuessNotEqualsSecretNumber_IncrementsGuessCounter()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string guess = "5678";

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
			const string secretNumber = "1234";
			const string invalidGuess = "123";

			// Act
			string output = ProcessGuessWithInvalidInput(_gameLogic, secretNumber, invalidGuess, ref initialGuessCount);

			// Assert
			Assert.AreEqual(0, initialGuessCount);
			StringAssert.Contains(output, "Invalid input. Please enter a 4-digit number.");
		}

		private static string ProcessGuessWithInvalidInput(GameLogic gameLogic, string secretNumber, string userInput, ref int numberOfGuesses)
		{
			using var sw = new StringWriter();
			// Simulate multiple invalid inputs
			using var sr = new StringReader($"{userInput}\n{userInput}\n{userInput}");
			Console.SetOut(sw);
			Console.SetIn(sr);

			gameLogic.ProcessGuess(secretNumber, ref numberOfGuesses);

			return sw.ToString();
		}

		[TestMethod]
		public void BullsAndCows_CorrectGuess_ReturnsBBBB()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "1234";

			// Act
			string feedback = GameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual("BBBB,", feedback);
		}

		[TestMethod]
		public void BullsAndCows_IncorrectGuess_ReturnsCorrectBullsAndCows()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "1568";

			// Act
			string feedback = GameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreNotEqual("BBBB,", feedback);
			Assert.IsTrue(feedback.Contains('B') || feedback.Contains('C'));
		}

		[TestMethod]
		public void BullsAndCows_CorrectCows_ReturnsCorrectCCCC()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "4321";

			// Act
			string feedback = GameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual(",CCCC", feedback);
		}

		[TestMethod]
		public void BullsAndCows_PartialMatch_ReturnsMixedBullsAndCows()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "1243";

			// Act
			string feedback = GameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual("BB,CC", feedback);
		}

		[TestMethod]
		public void BullsAndCows_EdgeCase_RepeatingDigits()
		{
			// Arrange
			const string secretNumber = "1122";
			const string guess = "2211";

			// Act
			string feedback = GameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

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