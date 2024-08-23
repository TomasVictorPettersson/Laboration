using Laboration.ConsoleUI.Interfaces;
using Laboration.GameFactory.Interfaces;
using Laboration.GameFlow.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Enums;
using Moq;

namespace Laboration.GameApplication.Tests
{
	[TestClass]
	public class ProgramTests
	{
		private readonly Mock<IGameSelector> _mockGameSelector = new();
		private readonly Mock<IGameFactory> _mockGameFactory = new();
		private readonly Mock<IGameFlowController> _mockGameFlowController = new();

		// Initializes the mocks and sets up the Program class before each test.

		[TestInitialize]
		public void Setup()
		{
			Program.GameSelector = _mockGameSelector.Object;
			Program.Factory = _mockGameFactory.Object;
		}

		// Verifies that RunGameLoop exits without creating or running game flow when the selected game type is 'Quit'.

		[TestMethod]
		public void RunGameLoop_ShouldBreak_WhenSelectedGameTypeIsQuit()
		{
			// Arrange
			_mockGameSelector.Setup(gs => gs.SelectGameType()).Returns(GameTypes.Quit);

			// Act
			Program.RunGameLoop();

			// Assert
			// Verify that SelectGameType was called once.
			_mockGameSelector.Verify(gs => gs.SelectGameType(), Times.Once);

			// Verify that CreateGameFlowController was not called.
			_mockGameFactory.Verify(f => f.CreateGameFlowController(), Times.Never);

			// Verify that ExecuteGameLoop was not called.
			_mockGameFlowController.Verify(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()), Times.Never);
		}
	}
}