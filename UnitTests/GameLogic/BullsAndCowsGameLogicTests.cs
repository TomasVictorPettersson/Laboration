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
	public class BullsAndCowsGameLogicTests
	{
		private BullsAndCowsGameLogic _gameLogic = null!;
		private readonly Mock<IHighScoreManager> _mockHighScoreManager = new();
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private readonly Mock<IValidation> _mockValidation = new();

		// Initializes the game logic and mocks before each test.

		[TestInitialize]
		public void Setup()
		{
			_gameLogic = new BullsAndCowsGameLogic(
				_mockHighScoreManager.Object,
				_mockConsoleUI.Object,
				_mockValidation.Object
			);
		}

		// Verifies that the MakeSecretNumber method generates a unique 4-digit number.

		[TestMethod]
		public void MakeSecretNumber_ShouldGenerateUnique4DigitNumber()
		{
			// Act
			string secretNumber = _gameLogic.MakeSecretNumber();

			// Assert
			Assert.AreEqual(GameConstants.SecretNumberLength, secretNumber.Length, "Secret number should be 4 digits long.");
			Assert.IsTrue(HasUniqueDigits(secretNumber), "Secret number should have unique digits.");
		}

		// Verifies that the GetGameType method returns BullsAndCows game type.

		[TestMethod]
		public void GetGameType_ShouldReturnBullsAndCows()
		{
			// Act
			GameTypes gameType = _gameLogic.GetGameType();

			// Assert
			Assert.AreEqual(GameTypes.BullsAndCows, gameType, "GetGameType should return BullsAndCows.");
		}

		// Helper method to determine if a number string has unique digits.
		private static bool HasUniqueDigits(string number)
		{
			HashSet<char> digits = new(number);
			return digits.Count == number.Length;
		}
	}
}