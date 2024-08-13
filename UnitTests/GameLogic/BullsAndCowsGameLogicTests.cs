using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Implementations;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using Library.ConstantsLibrary.Constants;
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
		public void MakeSecretNumber_ShouldReturnDifferentNumbers()
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
		public void ProcessGuess_ShouldIncrementCounter_ForCorrectGuess()
		{
			// Arrange
			int numberOfGuesses = 0;
			const string correctGuess = "1234";
			const string feedback = "BBBB,";

			// Set up the mocks
			_mockValidation.Setup(v => v.IsCorrectGuess(correctGuess, TestConstants.SecretNumber)).Returns(true);
			_mockConsoleUI.Setup(ui => ui.DisplayGuessFeedback(feedback));

			// Act
			bool result = _gameLogic.ProcessGuess(TestConstants.SecretNumber, correctGuess, ref numberOfGuesses);

			// Assert
			Assert.IsTrue(result, "ProcessGuess should return true for a correct guess.");
			Assert.AreEqual(1, numberOfGuesses, "Number of guesses should increment for a correct guess.");

			// Verify that feedback is displayed
			_mockConsoleUI.Verify(
				ui => ui.DisplayGuessFeedback(feedback),
				Times.Once,
				"DisplayGuessFeedback should be called once with the correct feedback for a correct guess."
			);
		}

		[TestMethod]
		public void ProcessGuess_ShouldIncrementCounter_ForIncorrectGuess()
		{
			// Arrange
			int numberOfGuesses = 0;
			const string inCorrectGuess = "5678";
			const string feedback = ",";

			// Set up the mocks
			_mockValidation.Setup(v => v.IsCorrectGuess(inCorrectGuess, TestConstants.SecretNumber)).Returns(false);
			_mockConsoleUI.Setup(ui => ui.DisplayGuessFeedback(feedback));

			// Act
			bool result = _gameLogic.ProcessGuess(TestConstants.SecretNumber, inCorrectGuess, ref numberOfGuesses);

			// Assert
			Assert.IsFalse(result, "ProcessGuess should return false for an incorrect guess.");
			Assert.AreEqual(1, numberOfGuesses, "Number of guesses should increment for an incorrect guess.");

			// Verify that feedback is displayed
			_mockConsoleUI.Verify(
				ui => ui.DisplayGuessFeedback(feedback),
				Times.Once,
				"DisplayGuessFeedback should be called once with the correct feedback for an incorrect guess."
			);
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnBBBB_ForCorrectGuess()
		{
			// Arrange
			const string guess = "1234";

			// Act
			string feedback = BullsAndCowsGameLogic.GenerateBullsAndCowsFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual("BBBB,", feedback, "Feedback should be 'BBBB,' for correct guess.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnCorrectFeedback_ForIncorrectGuess()
		{
			// Arrange
			const string guess = "1568";

			// Act
			string feedback = BullsAndCowsGameLogic.GenerateBullsAndCowsFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreNotEqual("BBBB,", feedback, "Feedback should not be 'BBBB,' for incorrect guess.");
			Assert.IsTrue(feedback.Contains('B') || feedback.Contains('C'), "Feedback should contain 'B' or 'C' for incorrect guess.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnCCCC_ForCorrectCows()
		{
			// Arrange
			const string guess = "4321";

			// Act
			string feedback = BullsAndCowsGameLogic.GenerateBullsAndCowsFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(",CCCC", feedback, "Feedback should be ',CCCC' for correct cows.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnMixedBullsAndCows_ForPartialMatch()
		{
			// Arrange
			const string guess = "1243";

			// Act
			string feedback = BullsAndCowsGameLogic.GenerateBullsAndCowsFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual("BB,CC", feedback, "Feedback should be 'BB,CC' for partial match.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnBB_ForOnlyBulls()
		{
			// Arrange
			const string guess = "1259";

			// Act
			string feedback = BullsAndCowsGameLogic.GenerateBullsAndCowsFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual("BB,", feedback, "Feedback should be 'BB,' for only bulls.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnCC_ForOnlyCows()
		{
			// Arrange
			const string guess = "3498";

			// Act
			string feedback = BullsAndCowsGameLogic.GenerateBullsAndCowsFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(",CC", feedback, "Feedback should be ',CC' for only cows.");
		}
	}
}