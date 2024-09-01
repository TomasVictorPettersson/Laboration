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

		// Verifies that ProcessGuess increments the counter and returns true for a correct guess,
		// and calls DisplayGuessFeedback with the correct feedback.
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

		// Verifies that ProcessGuess increments the counter and returns false for an incorrect guess,
		// and calls DisplayGuessFeedback with the correct feedback.
		[TestMethod]
		public void ProcessGuess_ShouldIncrementCounter_ForIncorrectGuess()
		{
			// Arrange
			int numberOfGuesses = 0;
			const string inCorrectGuess = "5678";

			// Set up the mocks
			_mockValidation.Setup(v => v.IsCorrectGuess(inCorrectGuess, TestConstants.SecretNumber)).Returns(false);
			_mockConsoleUI.Setup(ui => ui.DisplayGuessFeedback(GameConstants.FeedbackComma));

			// Act
			bool result = _gameLogic.ProcessGuess(TestConstants.SecretNumber, inCorrectGuess, ref numberOfGuesses);

			// Assert
			Assert.IsFalse(result, "ProcessGuess should return false for an incorrect guess.");
			Assert.AreEqual(1, numberOfGuesses, "Number of guesses should increment for an incorrect guess.");
			_mockConsoleUI.Verify(
				ui => ui.DisplayGuessFeedback(GameConstants.FeedbackComma),
				Times.Once,
				"DisplayGuessFeedback should be called once with the correct feedback for an incorrect guess."
			);
		}

		// Verifies that the CountBulls method returns the correct number of bulls for a given guess.
		[TestMethod]
		public void CountBulls_ShouldReturnCorrectNumberOfBulls()
		{
			// Arrange
			const string guess = "1212";

			// Act
			int bulls = _gameLogic.CountBulls(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(2, bulls, "CountBulls should return 2 for this guess.");
		}

		// Verifies that the CountCows method returns the correct number of cows for a given guess.
		[TestMethod]
		public void CountCows_ShouldReturnCorrectNumberOfCows()
		{
			// Arrange
			const string guess = "1325";

			// Act
			int cows = _gameLogic.CountCows(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(2, cows, "CountCows should return 2 for this guess.");
		}

		// Verifies that GenerateFeedback returns 'BBBB,' for a correct guess.
		[TestMethod]
		public void GenerateFeedback_ShouldReturnBBBB_ForCorrectGuess()
		{
			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, TestConstants.Guess);

			// Assert
			Assert.AreEqual(TestConstants.FeedbackBBBB, feedback, "Feedback should be 'BBBB,' for a correct guess.");
		}

		// Verifies that GenerateFeedback correctly handles guesses with repeating digits.
		[TestMethod]
		public void GenerateFeedback_ShouldHandleGuessWithRepeatingDigits()
		{
			// Arrange
			const string secretNumber = "5699";
			const string guess = "1299";

			// Act
			string feedback = _gameLogic.GenerateFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual(TestConstants.FeedbackBBComma, feedback, "Feedback should correctly handle guesses with repeating digits.");
		}

		// Verifies that GenerateFeedback returns feedback other than 'BBBB,' for an incorrect guess.
		[TestMethod]
		public void GenerateFeedback_ShouldReturnCorrectFeedback_ForIncorrectGuess()
		{
			// Arrange
			const string guess = "1568";

			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreNotEqual(TestConstants.FeedbackBBBB, feedback, "Feedback should not be 'BBBB,' for incorrect guess.");
			Assert.IsTrue(feedback.Contains(GameConstants.BullCharacter) || feedback.Contains(GameConstants.CowCharacter), "Feedback should contain 'B' or 'C' for incorrect guess.");
		}

		// Verifies that GenerateFeedback returns ',CCCC' for correct cows.
		[TestMethod]
		public void GenerateFeedback_ShouldReturnCCCC_ForCorrectCows()
		{
			// Arrange
			const string guess = "4321";

			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(TestConstants.FeedbackCCCC, feedback, "Feedback should be ',CCCC' for correct cows.");
		}

		// Verifies that GenerateFeedback returns 'BB,CC' for partial matches.
		[TestMethod]
		public void GenerateFeedback_ShouldReturnMixedBullsAndCows_ForPartialMatch()
		{
			// Arrange
			const string guess = "1243";

			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(TestConstants.FeedbackBBCC, feedback, "Feedback should be 'BB,CC' for partial match.");
		}

		// Verifies that GenerateFeedback returns 'BB,' for guesses with only bulls.
		[TestMethod]
		public void GenerateFeedback_ShouldReturnBB_ForOnlyBulls()
		{
			// Arrange
			const string guess = "1259";

			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(TestConstants.FeedbackBBComma, feedback, "Feedback should be 'BB,' for only bulls.");
		}

		// Verifies that GenerateFeedback returns ',CC' for guesses with only cows.
		[TestMethod]
		public void GenerateFeedback_ShouldReturnCC_ForOnlyCows()
		{
			// Arrange
			const string guess = "3498";

			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(TestConstants.FeedbackCommaCC, feedback, "Feedback should be ',CC' for only cows.");
		}

		// Verifies that GenerateFeedback returns ',' for guesses with no bulls or cows.
		[TestMethod]
		public void GenerateFeedback_ShouldReturnComma_ForNoBullsOrCows()
		{
			// Arrange
			const string guess = "5678";

			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(GameConstants.FeedbackComma, feedback, "Feedback should be ',' for no bulls or cows.");
		}
	}
}