using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Implementations;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Laboration.UnitTests.GameLogic
{
	[TestClass]
	public class BullsAndCowsGameLogicTests
	{
		private Mock<IHighScoreManager> _mockHighScoreManager;
		private Mock<IConsoleUI> _mockConsoleUI;
		private Mock<IValidation> _mockValidation;
		private BullsAndCowsGameLogic _gameLogic;

		[TestInitialize]
		public void TestInitialize()
		{
			// Arrange
			_mockHighScoreManager = new Mock<IHighScoreManager>();
			_mockConsoleUI = new Mock<IConsoleUI>();
			_mockValidation = new Mock<IValidation>();
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

			// Set up the mocks
			_mockValidation.Setup(v => v.IsCorrectGuess(correctGuess, It.IsAny<string>())).Returns(true);
			_mockConsoleUI.Setup(ui => ui.DisplayGuessFeedback(TestConstants.FeedbackBBBB));

			// Act
			bool result = _gameLogic.ProcessGuess(It.IsAny<string>(), correctGuess, ref numberOfGuesses);

			// Assert
			Assert.IsTrue(result, "ProcessGuess should return true for a correct guess.");
			Assert.AreEqual(1, numberOfGuesses, "Number of guesses should increment for a correct guess.");
			_mockConsoleUI.Verify(
				ui => ui.DisplayGuessFeedback(TestConstants.FeedbackBBBB),
				Times.Once,
				"DisplayGuessFeedback should be called once with the correct feedback for a correct guess."
			);
		}

		[TestMethod]
		public void ProcessGuess_ShouldIncrementCounter_ForIncorrectGuess()
		{
			// Arrange
			int numberOfGuesses = 0;
			const string incorrectGuess = "5678";

			// Set up the mocks
			_mockValidation.Setup(v => v.IsCorrectGuess(incorrectGuess, It.IsAny<string>())).Returns(false);
			_mockConsoleUI.Setup(ui => ui.DisplayGuessFeedback(TestConstants.FeedbackComma));

			// Act
			bool result = _gameLogic.ProcessGuess(It.IsAny<string>(), incorrectGuess, ref numberOfGuesses);

			// Assert
			Assert.IsFalse(result, "ProcessGuess should return false for an incorrect guess.");
			Assert.AreEqual(1, numberOfGuesses, "Number of guesses should increment for an incorrect guess.");
			_mockConsoleUI.Verify(
				ui => ui.DisplayGuessFeedback(TestConstants.FeedbackComma),
				Times.Once,
				"DisplayGuessFeedback should be called once with the correct feedback for an incorrect guess."
			);
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnBBBB_ForCorrectGuess()
		{
			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, TestConstants.Guess);

			// Assert
			Assert.AreEqual(TestConstants.FeedbackBBBB, feedback, "Feedback should be 'BBBB' for a correct guess.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnCorrectFeedback_ForIncorrectGuess()
		{
			// Arrange
			const string guess = "1568";

			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreNotEqual(TestConstants.FeedbackBBBB, feedback, "Feedback should not be 'BBBB' for an incorrect guess.");
			Assert.IsTrue(feedback.Contains('B') || feedback.Contains('C'), "Feedback should contain 'B' or 'C' for incorrect guess.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnCCCC_ForCorrectCows()
		{
			// Arrange
			const string guess = "4321";

			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(TestConstants.FeedbackCCCC, feedback, "Feedback should be 'CCCC' for correct cows.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnMixedBullsAndCows_ForPartialMatch()
		{
			// Arrange
			const string guess = "1243";

			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(TestConstants.FeedbackBBCC, feedback, "Feedback should be 'BB,CC' for partial match.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnBB_ForOnlyBulls()
		{
			// Arrange
			const string guess = "1259";

			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(TestConstants.FeedbackBBComma, feedback, "Feedback should be 'BB,' for only bulls.");
		}

		[TestMethod]
		public void BullsAndCowsFeedback_ShouldReturnCC_ForOnlyCows()
		{
			// Arrange
			const string guess = "3498";

			// Act
			string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

			// Assert
			Assert.AreEqual(TestConstants.FeedbackCommaCC, feedback, "Feedback should be ',CC' for only cows.");
		}
	}
}