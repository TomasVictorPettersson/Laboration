using ConsoleUI.Interfaces;
using GameLogic.Implementations;
using GameResources.Enums;
using HighScoreManagement.Interfaces;
using Moq;
using Validation.Interfaces;

namespace UnitTests.GameLogic
{
	[TestClass]
	public class MasterMindGameLogicTests
	{
		private MasterMindGameLogic _gameLogic = null!;
		private readonly Mock<IHighScoreManager> _mockHighScoreManager = new();
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private readonly Mock<IValidation> _mockValidation = new();

		// Initializes the game logic and mocks before each test.

		[TestInitialize]
		public void Setup()
		{
			_gameLogic = new MasterMindGameLogic(
				_mockHighScoreManager.Object,
				_mockConsoleUI.Object,
				_mockValidation.Object
			);
		}

		// Verifies that MakeSecretNumber generates a 4-digit number containing only digits.

		[TestMethod]
		public void MakeSecretNumber_ShouldGenerate4DigitNumber()
		{
			// Act
			string secretNumber = _gameLogic.MakeSecretNumber();

			// Assert
			Assert.AreEqual(4, secretNumber.Length, "Secret number should be 4 digits long.");
			Assert.IsTrue(IsDigitsOnly(secretNumber), "Secret number should contain only digits.");
		}

		// Verifies that GetGameType returns MasterMind.

		[TestMethod]
		public void GetGameType_ShouldReturnMasterMind()
		{
			// Act
			GameTypes gameType = _gameLogic.GetGameType();

			// Assert
			Assert.AreEqual(GameTypes.MasterMind, gameType, "GetGameType should return MasterMind.");
		}

		// Helper method to check if a string contains only digits.

		private static bool IsDigitsOnly(string input)
		{
			foreach (char c in input)
			{
				if (!char.IsDigit(c))
				{
					return false;
				}
			}
			return true;
		}
	}
}