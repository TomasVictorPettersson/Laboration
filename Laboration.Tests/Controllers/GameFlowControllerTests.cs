using Laboration.Business.Interfaces;
using Laboration.Controllers.Classes;
using Laboration.UI.Interfaces;
using Moq;

namespace Laboration.Tests.Controllers
{
	[TestClass]
	public class GameFlowControllerTests
	{
		private readonly Mock<IUserInterface> mockUserInterface = new();
		private readonly Mock<IGameLogic> mockGameLogic = new();

		[TestMethod]
		public void ExecuteGameLoop_PlaysOnce_WhenAskToContinueReturnsFalse()
		{
			// Arrange
			mockUserInterface.Setup(ui => ui.AskToContinue()).Returns(false);
			mockGameLogic.Setup(gl => gl.PlayGame(It.IsAny<string>()));

			// Act
			GameFlowController.ExecuteGameLoop(mockUserInterface.Object, mockGameLogic.Object);

			// Assert
			mockGameLogic.Verify(gl => gl.PlayGame(It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		public void ExecuteGameLoop_PlaysMultipleTimes_WhenAskToContinueReturnsTrue()
		{
			// Arrange
			mockUserInterface.SetupSequence(ui => ui.AskToContinue())
				.Returns(true)
				.Returns(true)
				.Returns(false);
			mockGameLogic.Setup(gl => gl.PlayGame(It.IsAny<string>()));

			// Act
			GameFlowController.ExecuteGameLoop(mockUserInterface.Object, mockGameLogic.Object);

			// Assert
			mockGameLogic.Verify(gl => gl.PlayGame(It.IsAny<string>()), Times.Exactly(3));
		}
	}
}