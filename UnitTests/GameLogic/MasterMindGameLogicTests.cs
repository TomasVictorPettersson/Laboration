using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Implementations;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using Moq;

namespace Laboration.UnitTests.GameLogic
{
	[TestClass]
	public class MasterMindGameLogicTests
	{
		private MasterMindGameLogic _gameLogic;
		private Mock<IHighScoreManager> _mockHighScoreManager;
		private Mock<IConsoleUI> _mockConsoleUI;
		private Mock<IValidation> _mockValidation;

		[TestInitialize]
		public void Setup()
		{
			_mockHighScoreManager = new Mock<IHighScoreManager>();
			_mockConsoleUI = new Mock<IConsoleUI>();
			_mockValidation = new Mock<IValidation>();
			_gameLogic = new MasterMindGameLogic(
				_mockHighScoreManager.Object,
				_mockConsoleUI.Object,
				_mockValidation.Object
			);
		}

		[TestMethod]
		public void MakeSecretNumber_ShouldGenerate4DigitNumber()
		{
			// Act
			string secretNumber = _gameLogic.MakeSecretNumber();

			// Assert
			Assert.AreEqual(4, secretNumber.Length, "Secret number should be 4 digits long.");
			Assert.IsTrue(IsDigitsOnly(secretNumber), "Secret number should contain only digits.");
		}

		[TestMethod]
		public void CountCows_ShouldReturnCorrectNumberOfCows()
		{
			// Arrange
			string secretNumber = "1122";
			string guess = "1212";

			// Act
			int cows = _gameLogic.CountCows(secretNumber, guess);

			// Assert
			Assert.AreEqual(2, cows, "CountCows should return 2 for this guess.");
		}

		[TestMethod]
		public void GetGameType_ShouldReturnMasterMind()
		{
			// Act
			GameTypes gameType = _gameLogic.GetGameType();

			// Assert
			Assert.AreEqual(GameTypes.MasterMind, gameType, "GetGameType should return MasterMind.");
		}

		private bool IsDigitsOnly(string input)
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