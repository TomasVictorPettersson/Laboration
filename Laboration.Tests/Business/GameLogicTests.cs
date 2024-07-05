using Laboration.Business.Classes;
using Laboration.Common.Interfaces;
using Laboration.UI.Interfaces;
using Moq;

namespace Laboration.Tests.Business
{
	[TestClass]
	public class GameLogicTests
	{
		private Mock<IHighScoreManager> _highScoreManagerMock;
		private Mock<IUserInterface> _userInterfaceMock;
		private GameLogic _gameLogic;

		[TestInitialize]
		public void Initialize()
		{
			_highScoreManagerMock = new Mock<IHighScoreManager>();
			_userInterfaceMock = new Mock<IUserInterface>();
			_gameLogic = new GameLogic(_highScoreManagerMock.Object, _userInterfaceMock.Object);
		}

		[TestMethod]
		public void MakeSecretNumber_GeneratesUnique4DigitNumber()
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
		public void GenerateBullsAndCowsFeedback_CorrectGuess_ReturnsBBBB()
		{
			// Arrange
			string secretNumber = "1234";
			string guess = "1234";

			// Act
			string feedback = _gameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual("BBBB,", feedback);
		}

		[TestMethod]
		public void GenerateBullsAndCowsFeedback_IncorrectGuess_ReturnsCorrectBullsAndCows()
		{
			// Arrange
			string secretNumber = "1234";
			string guess = "1568";
			// Act
			string feedback = _gameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreNotEqual("BBBB,", feedback);
		}
	}
}