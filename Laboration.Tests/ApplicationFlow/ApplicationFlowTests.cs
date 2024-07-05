using Laboration.ApplicationFlow.Classes;
using Laboration.Business.Classes;
using Laboration.Business.Interfaces;
using Laboration.Common.Interfaces;
using Laboration.UI.Interfaces;
using Moq;

namespace Laboration.Tests.ApplicationFlow
{
	[TestClass]
	public class ProgramTests
	{
		private Mock<IUserInterface> _mockUserInterface;
		private Mock<IHighScoreManager> _mockHighScoreManager;
		private Mock<IGameLogic> _mockGameLogic;

		[TestInitialize]
		public void Setup()
		{
			_mockUserInterface = new Mock<IUserInterface>();
			_mockHighScoreManager = new Mock<IHighScoreManager>();
			_mockGameLogic = new Mock<IGameLogic>();
		}

		[TestMethod]
		public void Main_ShouldPlayGameAndAskToContinue()
		{
			// Arrange
			_mockUserInterface.Setup(ui => ui.GetUserName()).Returns("TestUser");
			_mockUserInterface.SetupSequence(ui => ui.AskToContinue())
				.Returns(true)  // First iteration: continue
				.Returns(false); // Second iteration: stop

			var userInterface = _mockUserInterface.Object;
			var highScoreManager = _mockHighScoreManager.Object;
			var gameLogic = new GameLogic(highScoreManager, userInterface);

			// Act
			Program.Main();

			// Assert
			_mockUserInterface.Verify(ui => ui.GetUserName(), Times.Once);
			_mockUserInterface.Verify(ui => ui.AskToContinue(), Times.Exactly(2));
			_mockGameLogic.Verify(gl => gl.PlayGame("TestUser"), Times.Exactly(2));
		}

		[TestMethod]
		public void Main_ShouldStopPlayingWhenUserDoesNotContinue()
		{
			// Arrange
			_mockUserInterface.Setup(ui => ui.GetUserName()).Returns("TestUser");
			_mockUserInterface.Setup(ui => ui.AskToContinue()).Returns(false);

			var userInterface = _mockUserInterface.Object;
			var highScoreManager = _mockHighScoreManager.Object;
			var gameLogic = new GameLogic(highScoreManager, userInterface);

			// Act
			Program.Main();

			// Assert
			_mockUserInterface.Verify(ui => ui.GetUserName(), Times.Once);
			_mockUserInterface.Verify(ui => ui.AskToContinue(), Times.Once);
			_mockGameLogic.Verify(gl => gl.PlayGame("TestUser"), Times.Once);
		}
	}
}