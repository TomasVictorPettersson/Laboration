﻿using Laboration.ConsoleUI.Interfaces;
using Laboration.GameFlow.Implementations;
using Laboration.GameLogic.Interfaces;
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
			_mockConsoleUI.Setup(ui => ui.AskToContinue()).Returns(false);
			_mockGameLogic.Setup(gl => gl.PlayGame(It.IsAny<string>()));

			// Act
			_gameFlowController.ExecuteGameLoop(_mockConsoleUI.Object, _mockGameLogic.Object);

			// Assert
			_mockGameLogic.Verify(gl => gl.PlayGame(It.IsAny<string>()), Times.Once, "PlayGame should be called once when AskToContinue returns false.");
		}

		[TestMethod]
		public void ExecuteGameLoop_ShouldPlayMultipleTimes_WhenAskToContinueReturnsTrue()
		{
			// Arrange
			_mockConsoleUI.SetupSequence(ui => ui.AskToContinue())
				.Returns(true)
				.Returns(true)
				.Returns(false);
			_mockGameLogic.Setup(gl => gl.PlayGame(It.IsAny<string>()));

			// Act
			_gameFlowController.ExecuteGameLoop(_mockConsoleUI.Object, _mockGameLogic.Object);

			// Assert
			_mockGameLogic.Verify(gl => gl.PlayGame(It.IsAny<string>()), Times.Exactly(3), "PlayGame should be called three times when AskToContinue returns true twice and then false.");
		}
	}
}