using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laboration.Controllers.Classes;
using Laboration.Business.Interfaces;
using Laboration.Common.Interfaces;
using Moq;
using Laboration.UI.Interfaces;

namespace Laboration.Tests.Controllers
{
	[TestClass]
	public class GameFlowControllerTests
	{
		private Mock<IUserInterface> mockUserInterface;
		private Mock<IHighScoreManager> mockHighScoreManager;
		private Mock<IGameLogic> mockGameLogic;

		[TestInitialize]
		public void Initialize()
		{
			mockUserInterface = new Mock<IUserInterface>();
			mockHighScoreManager = new Mock<IHighScoreManager>();
			mockGameLogic = new Mock<IGameLogic>();
		}

		[TestMethod]
		public void TestExecuteGameLoop_PlaysGameOnce_WhenAskToContinueReturnsFalse()
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
		public void TestExecuteGameLoop_PlaysGameMultipleTimes_WhenAskToContinueReturnsTrue()
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
			mockGameLogic.Verify(gl => gl.PlayGame(It.IsAny<string>()), Times.Exactly(2));
		}
	}
}