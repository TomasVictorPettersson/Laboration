using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Implementations;
using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using Moq;

namespace Laboration.UnitTests.GameLogic
{
	[TestClass]
	public class GameLogicBaseTests
	{
		private readonly Mock<IHighScoreManager> _mockHighScoreManager = new();
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private readonly Mock<IValidation> _mockValidation = new();
		private BullsAndCowsGameLogic _gameLogic = null!;

		[TestInitialize]
		public void TestInitialize()
		{
			_gameLogic = new BullsAndCowsGameLogic(
				_mockHighScoreManager.Object,
				_mockConsoleUI.Object,
				_mockValidation.Object
			);
		}

		[TestMethod]
		public void PlayGameLoop_ShouldEndWhenGuessIsCorrect()
		{
			// Arrange
			_mockConsoleUI.Setup(ui => ui.GetValidGuessFromUser(GameTypes.BullsAndCows)).Returns(TestConstants.SecretNumber);
			_mockValidation.Setup(v => v.IsCorrectGuess(TestConstants.SecretNumber, TestConstants.SecretNumber)).Returns(true);

			// Act
			_gameLogic.PlayGameLoop(TestConstants.SecretNumber, TestConstants.UserName);

			// Assert
			_mockConsoleUI.Verify(ui => ui.GetValidGuessFromUser(GameTypes.BullsAndCows), Times.AtLeastOnce);
			_mockValidation.Verify(v => v.IsCorrectGuess(TestConstants.SecretNumber, TestConstants.SecretNumber), Times.AtLeastOnce);
		}

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

		[TestMethod]
		public void EndGame_ShouldSaveResultAndDisplayMessages()
		{
			// Arrange
			const int numberOfGuesses = 1;
			_mockHighScoreManager.Setup(h => h.SaveResult(TestConstants.UserName, numberOfGuesses));
			_mockConsoleUI.Setup(ui => ui.DisplayCorrectMessage(TestConstants.SecretNumber, numberOfGuesses));
			_mockConsoleUI.Setup(ui => ui.DisplayHighScoreList(TestConstants.UserName));

			// Act
			_gameLogic.EndGame(TestConstants.SecretNumber, TestConstants.UserName, numberOfGuesses);

			// Assert
			_mockHighScoreManager.Verify(h => h.SaveResult(TestConstants.UserName, numberOfGuesses), Times.Once);
			_mockConsoleUI.Verify(ui => ui.DisplayCorrectMessage(TestConstants.SecretNumber, numberOfGuesses), Times.Once);
			_mockConsoleUI.Verify(ui => ui.DisplayHighScoreList(TestConstants.UserName), Times.Once);
		}

		// Verifies that ProcessGuess increments the counter and returns true for a correct guess.
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

		// Verifies that ProcessGuess increments the counter and returns false for an incorrect guess.
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