using Laboration.Business.Interfaces;
using Laboration.Controllers.Classes;
using Laboration.UI.Interfaces;
using Moq;

namespace Laboration.Tests.Controllers
{
	[TestClass]
	public class GameFlowControllerTests
	{
		private Mock<IUserInterface> _mockUserInterface;
		private Mock<IGameLogic> _mockGameLogic;

		[TestInitialize]
		public void Setup()
		{
			_mockUserInterface = new Mock<IUserInterface>();
			_mockGameLogic = new Mock<IGameLogic>();
		}

		[TestMethod]
		public void ExecuteGameLoop_ShouldPlayGameAndAskToContinue()
		{
			_mockUserInterface.Setup(ui => ui.GetUserName()).Returns("TestUser");
			_mockUserInterface.SetupSequence(ui => ui.AskToContinue())
				.Returns(true)
				.Returns(false);

			GameFlowController.ExecuteGameLoop(_mockUserInterface.Object, _mockGameLogic.Object);

			_mockUserInterface.Verify(ui => ui.GetUserName(), Times.Once);
			_mockUserInterface.Verify(ui => ui.AskToContinue(), Times.Exactly(2));
			_mockGameLogic.Verify(gl => gl.PlayGame("TestUser"), Times.Exactly(2));
		}

		[TestMethod]
		public void ExecuteGameLoop_ShouldStopPlayingWhenUserDoesNotContinue()
		{
			_mockUserInterface.Setup(ui => ui.GetUserName()).Returns("TestUser");
			_mockUserInterface.Setup(ui => ui.AskToContinue()).Returns(false);

			GameFlowController.ExecuteGameLoop(_mockUserInterface.Object, _mockGameLogic.Object);

			_mockUserInterface.Verify(ui => ui.GetUserName(), Times.Once);
			_mockUserInterface.Verify(ui => ui.AskToContinue(), Times.Once);
			_mockGameLogic.Verify(gl => gl.PlayGame("TestUser"), Times.Once);
		}
	}
}