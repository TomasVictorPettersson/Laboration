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
		private Mock<IHighScoreManager> _mockHighScoreManager;
		private Mock<IConsoleUI> _mockConsoleUI;
		private Mock<IValidation> _mockValidation;
		private TestGameLogic _gameLogic;

		[TestInitialize]
		public void TestInitialize()
		{
			_mockHighScoreManager = new Mock<IHighScoreManager>();
			_mockConsoleUI = new Mock<IConsoleUI>();
			_mockValidation = new Mock<IValidation>();
			_gameLogic = new TestGameLogic(
				_mockHighScoreManager.Object,
				_mockConsoleUI.Object,
				_mockValidation.Object
			);
		}

		[TestMethod]
		public void PlayGame_ShouldDisplayWelcomeMessage()
		{
			// Arrange
			const bool isNewGame = true;
			_mockConsoleUI.Setup(ui => ui.DisplayWelcomeMessage(GameTypes.Test, TestConstants.UserName, isNewGame));

			// Act
			_gameLogic.PlayGame(TestConstants.UserName, isNewGame);

			// Assert
			_mockConsoleUI.Verify(
				ui => ui.DisplayWelcomeMessage(GameTypes.Test, TestConstants.UserName, isNewGame),
				Times.Once
			);
		}

		[TestMethod]
		public void PlayGameLoop_ShouldEndWhenGuessIsCorrect()
		{
			// Arrange
			_mockConsoleUI.Setup(ui => ui.GetValidGuessFromUser(GameTypes.Test)).Returns(TestConstants.SecretNumber);
			_mockValidation.Setup(v => v.IsCorrectGuess(TestConstants.SecretNumber, TestConstants.SecretNumber)).Returns(true);

			// Act
			_gameLogic.PlayGameLoop(TestConstants.SecretNumber, TestConstants.UserName);

			// Assert
			_mockConsoleUI.Verify(ui => ui.GetValidGuessFromUser(GameTypes.Test), Times.AtLeastOnce);
			_mockValidation.Verify(v => v.IsCorrectGuess(TestConstants.SecretNumber, TestConstants.SecretNumber), Times.AtLeastOnce);
		}

		[TestMethod]
		public void HandleUserGuess_ShouldReturnTrueForCorrectGuess()
		{
			// Arrange
			int numberOfGuesses = 0;
			_mockConsoleUI.Setup(ui => ui.GetValidGuessFromUser(GameTypes.Test)).Returns(TestConstants.Guess);
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

		// Define a test-specific implementation of GameLogicBase for testing.
		private class TestGameLogic : GameLogicBase
		{
			public TestGameLogic(IHighScoreManager highScoreManager, IConsoleUI consoleUI, IValidation validation)
				: base(highScoreManager, consoleUI, validation)
			{
			}

			public override string MakeSecretNumber() => TestConstants.SecretNumber;

			public override int CountCows(string secretNumber, string guess) => 0;

			public override GameTypes GetGameType() => GameTypes.Test;

			public override string GenerateFeedback(string secretNumber, string guess) => "Feedback";

			public override int CountBulls(string secretNumber, string guess) => 0;
		}
	}
}