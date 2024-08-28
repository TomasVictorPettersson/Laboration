using ConsoleUI.Interfaces;
using GameLogic.Implementations;
using GameResources.Constants;
using GameResources.Enums;
using HighScoreManagement.Interfaces;
using Moq;
using Validation.Interfaces;

namespace UnitTests.GameLogic
{
	[TestClass]
	public class GameLogicBaseTests
	{
		private readonly Mock<IHighScoreManager> _mockHighScoreManager = new();
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private readonly Mock<IValidation> _mockValidation = new();
		private BullsAndCowsGameLogic _gameLogic = null!;

		// Initializes the game logic and mocks before each test.

		[TestInitialize]
		public void TestInitialize()
		{
			_gameLogic = new BullsAndCowsGameLogic(
				_mockHighScoreManager.Object,
				_mockConsoleUI.Object,
				_mockValidation.Object
			);
		}

		// Verifies that HandleUserGuess returns true and increments the number of guesses when the guess is correct.

		[TestMethod]
		public void HandleUserGuess_ShouldReturnTrueForCorrectGuess()
		{
			// Arrange
			int numberOfGuesses = 0;
			_mockConsoleUI.Setup(ui => ui.GetValidGuessFromUser(GameTypes.BullsAndCows)).Returns(TestConstants.Guess);
			_mockValidation.Setup(v => v.IsCorrectGuess(TestConstants.Guess, TestConstants.SecretNumber)).Returns(true);

			// Act
			bool result = _gameLogic.HandleUserGuess(TestConstants.SecretNumber, ref numberOfGuesses);

			// Assert
			Assert.IsTrue(result, "HandleUserGuess should return true for a correct guess.");
			Assert.AreEqual(1, numberOfGuesses, "Number of guesses should be incremented.");
		}

		// Verifies that ProcessGuess returns false and increments the number of guesses for an incorrect guess.

		[TestMethod]
		public void ProcessGuess_ShouldUpdateGuessCount()
		{
			// Arrange
			const string guess = "5678";
			int numberOfGuesses = 0;
			_mockValidation.Setup(v => v.IsCorrectGuess(guess, TestConstants.SecretNumber)).Returns(false);

			// Act
			bool result = _gameLogic.ProcessGuess(TestConstants.SecretNumber, guess, ref numberOfGuesses);

			// Assert
			Assert.IsFalse(result, "ProcessGuess should return false for an incorrect guess.");
			Assert.AreEqual(1, numberOfGuesses, "Number of guesses should be incremented.");
		}

		// Verifies that ProcessGuess increments the counter and returns true
		// for a correct guess, and calls DisplayGuessFeedback with the correct feedback.

		[TestMethod]
		public void ProcessGuess_ShouldIncrementCounter_ForCorrectGuess()
		{
			// Arrange
			int numberOfGuesses = 0;

			// Set up the mocks
			_mockValidation.Setup(v => v.IsCorrectGuess(TestConstants.Guess, TestConstants.SecretNumber)).Returns(true);
			_mockConsoleUI.Setup(ui => ui.DisplayGuessFeedback(TestConstants.FeedbackBBBB));

			// Act
			bool result = _gameLogic.ProcessGuess(TestConstants.SecretNumber, TestConstants.Guess, ref numberOfGuesses);

			// Assert
			Assert.IsTrue(result, "ProcessGuess should return true for a correct guess.");
			Assert.AreEqual(1, numberOfGuesses, "Number of guesses should increment for a correct guess.");
			_mockConsoleUI.Verify(
				ui => ui.DisplayGuessFeedback(TestConstants.FeedbackBBBB),
				Times.Once,
				"DisplayGuessFeedback should be called once with the correct feedback for a correct guess."
			);
		}

		// Verifies that ProcessGuess increments the counter and returns false
		// for an incorrect guess, and calls DisplayGuessFeedback with the correct feedback.

		[TestMethod]
		public void ProcessGuess_ShouldIncrementCounter_ForIncorrectGuess()
		{
			// Arrange
			int numberOfGuesses = 0;
			const string inCorrectGuess = "5678";

			// Set up the mocks
			_mockValidation.Setup(v => v.IsCorrectGuess(inCorrectGuess, TestConstants.SecretNumber)).Returns(false);
			_mockConsoleUI.Setup(ui => ui.DisplayGuessFeedback(TestConstants.FeedbackComma));

			// Act
			bool result = _gameLogic.ProcessGuess(TestConstants.SecretNumber, inCorrectGuess, ref numberOfGuesses);

			// Assert
			Assert.IsFalse(result, "ProcessGuess should return false for an incorrect guess.");
			Assert.AreEqual(1, numberOfGuesses, "Number of guesses should increment for an incorrect guess.");
			_mockConsoleUI.Verify(
				ui => ui.DisplayGuessFeedback(TestConstants.FeedbackComma),
				Times.Once,
				"DisplayGuessFeedback should be called once with the correct feedback for an incorrect guess."
			);
		}
	}
}