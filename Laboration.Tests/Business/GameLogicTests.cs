﻿using Laboration.Business.Classes;
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

		// Testing MakeSecretNumber
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
		public void ProcessGuess_InvalidInput_NullGuess_DoesNotIncrementGuessCounter()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string invalidGuess = null!;

			// Set up the mock to return an invalid guess (null)
			_userInterfaceMock.Setup(ui => ui.GetValidGuessFromUser(It.IsAny<int>())).Returns(invalidGuess);
			_userInterfaceMock.Setup(ui => ui.IsInputValid(invalidGuess)).Returns(false);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(0, initialGuessCount);
		}

		[TestMethod]
		public void ProcessGuess_InvalidInput_EmptyGuess_DoesNotIncrementGuessCounter()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string invalidGuess = "";

			// Set up the mock to return an invalid guess (empty string)
			_userInterfaceMock.Setup(ui => ui.GetValidGuessFromUser(It.IsAny<int>())).Returns(invalidGuess);
			_userInterfaceMock.Setup(ui => ui.IsInputValid(invalidGuess)).Returns(false);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(0, initialGuessCount);
		}

		[TestMethod]
		public void ProcessGuess_InvalidInput_Letters_DoesNotIncrementGuessCounter()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string invalidGuess = "test";

			// Set up the mock to return the invalid guess
			_userInterfaceMock.Setup(ui => ui.GetValidGuessFromUser(It.IsAny<int>())).Returns(invalidGuess);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(0, initialGuessCount);
		}

		[TestMethod]
		public void ProcessGuess_InvalidInput_RepeatingDigits_DoesNotIncrementGuessCounter()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string invalidGuess = "1122";

			// Set up the mock to return the invalid guess
			_userInterfaceMock.Setup(ui => ui.GetValidGuessFromUser(It.IsAny<int>())).Returns(invalidGuess);
			_userInterfaceMock.Setup(ui => ui.IsInputValid(invalidGuess)).Returns(false);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(0, initialGuessCount);
		}

		[TestMethod]
		public void ProcessGuess_GuessEqualsSecretNumber_IncrementsGuessCounter()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string guess = "1234";

			// Set up the mock to return the guess and validate it
			_userInterfaceMock.Setup(ui => ui.GetValidGuessFromUser(It.IsAny<int>())).Returns(guess);
			_userInterfaceMock.Setup(ui => ui.IsInputValid(guess)).Returns(true);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(1, initialGuessCount);
		}

		[TestMethod]
		public void ProcessGuess_GuessNotEqualsSecretNumber_IncrementsGuessCounter()
		{
			// Arrange
			int initialGuessCount = 0;
			const string secretNumber = "1234";
			const string guess = "5678";

			// Set up the mock to return the guess
			_userInterfaceMock.Setup(ui => ui.GetValidGuessFromUser(It.IsAny<int>())).Returns(guess);
			_userInterfaceMock.Setup(ui => ui.IsInputValid(guess)).Returns(true);

			// Act
			_gameLogic.ProcessGuess(secretNumber, ref initialGuessCount);

			// Assert
			Assert.AreEqual(1, initialGuessCount);
		}

		// Testing GenerateBullsAndCowsFeedback
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
		public void BullsAndCows_OnlyBulls_ReturnsBB()
		{
			// Arrange
			const string secretNumber = "9151";
			const string guess = "9142";

			// Act
			string feedback = GameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual("BB,", feedback);
		}

		[TestMethod]
		public void BullsAndCows_OnlyCows_ReturnsCC()
		{
			// Arrange
			const string secretNumber = "9151";
			const string guess = "1527";

			// Act
			string feedback = GameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual(",CC", feedback);
		}
	}
}