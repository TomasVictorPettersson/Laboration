using Laboration.ApplicationFlow.Classes;
using Laboration.Business.Interfaces;
using Laboration.UI.Interfaces;
using Moq;

namespace Laboration.Tests.ApplicationFlow
{
	[TestClass]
	public class GameRunnerTests
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
		public void Main_ShouldPlayGameAndAskToContinue()
		{
			_mockUserInterface.Setup(ui => ui.GetUserName()).Returns("TestUser");
			_mockUserInterface.SetupSequence(ui => ui.AskToContinue())
				.Returns(true)
				.Returns(false);

			// Call the static Main method directly with mocks
			GameRunner.Main();

			_mockUserInterface.Verify(ui => ui.GetUserName(), Times.Once);
			_mockUserInterface.Verify(ui => ui.AskToContinue(), Times.Exactly(2));
			_mockGameLogic.Verify(gl => gl.PlayGame("TestUser"), Times.Exactly(2));
		}

		[TestMethod]
		public void Main_ShouldStopPlayingWhenUserDoesNotContinue()
		{
			_mockUserInterface.Setup(ui => ui.GetUserName()).Returns("TestUser");
			_mockUserInterface.Setup(ui => ui.AskToContinue()).Returns(false);

			// Call the static Main method directly with mocks
			GameRunner.Main();

			_mockUserInterface.Verify(ui => ui.GetUserName(), Times.Once);
			_mockUserInterface.Verify(ui => ui.AskToContinue(), Times.Once);
			_mockGameLogic.Verify(gl => gl.PlayGame("TestUser"), Times.Once);
		}
	}
}