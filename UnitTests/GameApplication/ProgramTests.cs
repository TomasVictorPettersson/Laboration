using Laboration.ConsoleUI.Interfaces;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameApplication;
using Laboration.GameFactory.Interfaces;
using Laboration.GameFlow.Interfaces;
using Laboration.GameLogic.Interfaces;
using Moq;

namespace Laboration.UnitTests.GameApplication
{
	[TestClass]
	public class ProgramTests
	{
		private Mock<IDependencyInitializer>? _mockDependencyInitializer;
		private Mock<IGameFlowController>? _mockGameFlowController;
		private Mock<IConsoleUI>? _mockConsoleUI;
		private Mock<IGameLogic>? _mockGameLogic;
		private Mock<IGameFactory>? _mockGameFactory;

		[TestInitialize]
		public void SetUp()
		{
			// Initialize mocks for dependencies and game flow controller.
			_mockDependencyInitializer = new Mock<IDependencyInitializer>();
			_mockGameFlowController = new Mock<IGameFlowController>();
			_mockConsoleUI = new Mock<IConsoleUI>();
			_mockGameLogic = new Mock<IGameLogic>();
			_mockGameFactory = new Mock<IGameFactory>();

			// Set up the dependency initializer mock to return mocked interfaces.
			_mockDependencyInitializer
				.Setup(di => di.InitializeDependencies())
				.Returns((_mockConsoleUI.Object, _mockGameLogic.Object));

			// Set up the game factory mock to return the dependency initializer and game flow controller mocks.
			_mockGameFactory
				.Setup(factory => factory.CreateDependencyInitializer())
				.Returns(_mockDependencyInitializer.Object);
			_mockGameFactory
				.Setup(factory => factory.CreateGameFlowController())
				.Returns(_mockGameFlowController.Object);

			// Configure Program to use mocks for testing.
			Program.Factory = _mockGameFactory.Object;
		}

		[TestMethod]
		public void Main_InitializesDependencies()
		{
			// Act: Call the Main method to verify dependency initialization.
			Program.Main();

			// Assert: Verify that InitializeDependencies was called once.
			_mockDependencyInitializer!.Verify(di => di.InitializeDependencies(), Times.Once, "Dependencies should be initialized once.");
		}

		[TestMethod]
		public void Main_ExecutesGameLoop()
		{
			// Arrange: Redirect console output for verification.
			var consoleOutput = new StringWriter();
			Console.SetOut(consoleOutput);

			// Act: Call the Main method to verify game loop execution.
			Program.Main();

			// Assert: Verify that ExecuteGameLoop was called once.
			_mockGameFlowController!.Verify(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()), Times.Once, "Game loop should be executed once.");
		}

		[TestMethod]
		public void Main_HandlesExceptions()
		{
			// Arrange: Redirect console output for verification and configure mock to throw exception.
			var originalConsoleOut = Console.Out;
			var consoleOutput = new StringWriter();
			Console.SetOut(consoleOutput);

			_mockGameFlowController!
				.Setup(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()))
				.Throws(new Exception("Test exception"));

			// Act: Call the Main method to verify exception handling.
			Program.Main();

			// Assert: Verify that the error message was displayed correctly.
			Assert.IsTrue(consoleOutput.ToString().Contains("An error occurred:"), "Error message was not displayed correctly.");

			// Restore original console output.
			Console.SetOut(originalConsoleOut);
		}
	}
}