using Laboration.ConsoleUI.Interfaces;
using Laboration.GameFlow.Implementations;
using Laboration.GameLogic.Interfaces;
using Library.ConstantsLibrary.Constants;
using Moq;

namespace Laboration.UnitTests.GameFlow
{
	[TestClass]
	public class BullsAndCowsGameFlowControllerTests
	{
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private readonly Mock<IGameLogic> _mockGameLogic = new();
		private readonly BullsAndCowsGameFlowController _gameFlowController = new();

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
			_mockConsoleUI.Verify(ui => ui.DisplayGoodbyeMessage(TestConstants.UserName), Times.Once, "DisplayGoodbyeMessage should be called once after the game loop ends.");
		}

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

			_mockConsoleUI.Setup(ui => ui.DisplayGoodbyeMessage(TestConstants.UserName));

			// Act
			_gameFlowController.ExecuteGameLoop(_mockConsoleUI.Object, _mockGameLogic.Object);

			// Assert
			_mockGameLogic.Verify(gl => gl.PlayGame(TestConstants.UserName, true), Times.Once, "PlayGame should be called once for the first game.");
			_mockGameLogic.Verify(gl => gl.PlayGame(TestConstants.UserName, false), Times.Exactly(2), "PlayGame should be called twice for the subsequent games.");
			_mockConsoleUI.Verify(ui => ui.DisplayGoodbyeMessage(TestConstants.UserName), Times.Once, "DisplayGoodbyeMessage should be called once after the game loop ends.");
		}
	}
}