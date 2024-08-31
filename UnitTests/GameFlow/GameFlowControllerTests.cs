using ConsoleUI.Interfaces;
using GameFlow.Implementations;
using GameLogic.Interfaces;
using GameResources.Constants;
using GameResources.Enums;
using Moq;

namespace UnitTests.GameFlow
{
	[TestClass]
	public class GameFlowControllerTests
	{
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private readonly Mock<IGameLogic> _mockGameLogic = new();
		private readonly GameFlowController _gameFlowController = new(GameTypes.Test);

		// Verifies that ExecuteGameLoop plays the game once and displays a
		// goodbye message when AskToContinue returns false.
		[TestMethod]
		public void ExecuteGameLoop_ShouldPlayOnce_WhenAskToContinueReturnsFalse()
		{
			// Arrange
			_mockConsoleUI.Setup(ui => ui.GetUserName()).Returns(TestConstants.UserName);
			_mockConsoleUI.Setup(ui => ui.AskToContinue()).Returns(false);
			_mockGameLogic.Setup(gl => gl.PlayGame(TestConstants.UserName, true));

			// Act
			_gameFlowController.ExecuteGameLoop(_mockConsoleUI.Object, _mockGameLogic.Object);

			// Assert
			_mockGameLogic.Verify(gl => gl.PlayGame(TestConstants.UserName, true), Times.Once, "PlayGame should be called once when AskToContinue returns false.");
			_mockConsoleUI.Verify(ui => ui.DisplayGoodbyeMessage(GameTypes.Test, TestConstants.UserName), Times.Once, "DisplayGoodbyeMessage should be called once after the game loop ends.");
		}

		// Verifies that ExecuteGameLoop plays the game multiple times and displays a
		// goodbye message when AskToContinue returns true initially and then false.
		[TestMethod]
		public void ExecuteGameLoop_ShouldPlayMultipleTimes_WhenAskToContinueReturnsTrue()
		{
			// Arrange
			_mockConsoleUI.Setup(ui => ui.GetUserName()).Returns(TestConstants.UserName);
			_mockConsoleUI.SetupSequence(ui => ui.AskToContinue())
				.Returns(true)
				.Returns(true)
				.Returns(false);

			_mockGameLogic.Setup(gl => gl.PlayGame(TestConstants.UserName, true)).Verifiable();
			_mockGameLogic.Setup(gl => gl.PlayGame(TestConstants.UserName, false)).Verifiable();

			_mockConsoleUI.Setup(ui => ui.DisplayGoodbyeMessage(GameTypes.Test, TestConstants.UserName));

			// Act
			_gameFlowController.ExecuteGameLoop(_mockConsoleUI.Object, _mockGameLogic.Object);

			// Assert
			_mockGameLogic.Verify(gl => gl.PlayGame(TestConstants.UserName, true), Times.Once, "PlayGame should be called once for the first game.");
			_mockGameLogic.Verify(gl => gl.PlayGame(TestConstants.UserName, false), Times.Exactly(2), "PlayGame should be called twice for the subsequent games.");
			_mockConsoleUI.Verify(ui => ui.DisplayGoodbyeMessage(GameTypes.Test, TestConstants.UserName), Times.Once, "DisplayGoodbyeMessage should be called once after the game loop ends.");
		}

		// Verifies that the GameType is correctly set to the Test game type.
		[TestMethod]
		public void GameFlowController_ShouldHaveCorrectGameType()
		{
			// Assert
			Assert.AreEqual(GameTypes.Test, _gameFlowController.GameType, "GameType should be set to Test.");
		}
	}
}