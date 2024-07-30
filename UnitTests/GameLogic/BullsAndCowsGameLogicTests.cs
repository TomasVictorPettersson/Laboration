using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Implementations;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using Moq;

namespace Laboration.UnitTests.GameLogic
{
	[TestClass]
	public class BullsAndCowsGameLogicTests
	{
		private readonly Mock<IHighScoreManager> _mockHighScoreManager = new();
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private readonly Mock<IValidation> _mockValidation = new();
		private readonly BullsAndCowsGameLogic _gameLogic;

		public BullsAndCowsGameLogicTests()
		{
			_gameLogic = new BullsAndCowsGameLogic(_mockHighScoreManager.Object, _mockConsoleUI.Object, _mockValidation.Object);
		}

		[TestMethod]
		public void GenerateUnique4DigitNumber_ShouldReturnDifferentNumbers()
		{
			// Arrange
			string secretNumber1 = _gameLogic.MakeSecretNumber();
			string secretNumber2 = _gameLogic.MakeSecretNumber();

			// Act and Assert
			Assert.AreNotEqual(secretNumber1, secretNumber2, "Generated numbers should be unique.");
			Assert.IsTrue(secretNumber1.Length == 4 && secretNumber2.Length == 4, "Numbers should be 4 digits long.");
			Assert.IsTrue(int.TryParse(secretNumber1, out _) && int.TryParse(secretNumber2, out _), "Numbers should be valid integers.");
		}

		[TestMethod]
		public void ProcessGuess_ShouldNotIncrementCounter_ForNullGuess()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string invalidGuess = null!;

			_mockConsoleUI.Setup(ui => ui.GetValidGuessFromUser()).Returns(invalidGuess);
			_mockValidation.Setup(ui => ui.IsInputValid(invalidGuess)).Returns(false);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(0, initialGuessCount, "Guess counter should not increment for null input.");
		}

		[TestMethod]
		public void ProcessGuess_ShouldNotIncrementCounter_ForEmptyGuess()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string invalidGuess = "";

			_mockConsoleUI.Setup(ui => ui.GetValidGuessFromUser()).Returns(invalidGuess);
			_mockValidation.Setup(ui => ui.IsInputValid(invalidGuess)).Returns(false);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(0, initialGuessCount, "Guess counter should not increment for empty input.");
		}

		[TestMethod]
		public void ProcessGuess_ShouldNotIncrementCounter_ForLetters()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string invalidGuess = "test";

			_mockConsoleUI.Setup(ui => ui.GetValidGuessFromUser()).Returns(invalidGuess);
			_mockValidation.Setup(ui => ui.IsInputValid(invalidGuess)).Returns(false);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(0, initialGuessCount, "Guess counter should not increment for letter input.");
		}

		[TestMethod]
		public void ProcessGuess_ShouldNotIncrementCounter_ForRepeatingDigits()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string invalidGuess = "1122";

			_mockConsoleUI.Setup(ui => ui.GetValidGuessFromUser()).Returns(invalidGuess);
			_mockValidation.Setup(ui => ui.IsInputValid(invalidGuess)).Returns(false);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(0, initialGuessCount, "Guess counter should not increment for guesses with repeating digits.");
		}

		[TestMethod]
		public void ProcessGuess_ShouldIncrementCounter_ForCorrectGuess()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string guess = "1234";

			_mockConsoleUI.Setup(ui => ui.GetValidGuessFromUser()).Returns(guess);
			_mockValidation.Setup(ui => ui.IsInputValid(guess)).Returns(true);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(1, initialGuessCount, "Guess counter should increment for correct guess.");
		}

		[TestMethod]
		public void ProcessGuess_ShouldIncrementCounter_ForIncorrectGuess()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string guess = "5678";

			_mockConsoleUI.Setup(ui => ui.GetValidGuessFromUser()).Returns(guess);
			_mockValidation.Setup(ui => ui.IsInputValid(guess)).Returns(true);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(1, initialGuessCount, "Guess counter should increment for incorrect guess.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnBBBB_ForCorrectGuess()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "1234";

			// Act
			string feedback = BullsAndCowsGameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual("BBBB,", feedback, "Feedback should be 'BBBB,' for correct guess.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnCorrectFeedback_ForIncorrectGuess()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "1568";

			// Act
			string feedback = BullsAndCowsGameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreNotEqual("BBBB,", feedback, "Feedback should not be 'BBBB,' for incorrect guess.");
			Assert.IsTrue(feedback.Contains('B') || feedback.Contains('C'), "Feedback should contain 'B' or 'C' for incorrect guess.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnCCCC_ForCorrectCows()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "4321";

			// Act
			string feedback = BullsAndCowsGameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual(",CCCC", feedback, "Feedback should be ',CCCC' for correct cows.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnMixedBullsAndCows_ForPartialMatch()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "1243";

			// Act
			string feedback = BullsAndCowsGameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual("BB,CC", feedback, "Feedback should be 'BB,CC' for partial match.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnBB_ForOnlyBulls()
		{
			// Arrange
			const string secretNumber = "9151";
			const string guess = "9142";

			// Act
			string feedback = BullsAndCowsGameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual("BB,", feedback, "Feedback should be 'BB,' for only bulls.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnCC_ForOnlyCows()
		{
			// Arrange
			const string secretNumber = "9151";
			const string guess = "1527";

			// Act
			string feedback = BullsAndCowsGameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual(",CC", feedback, "Feedback should be ',CC' for only cows.");
		}
	}
}