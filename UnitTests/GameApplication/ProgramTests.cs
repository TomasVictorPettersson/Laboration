using Laboration.ConsoleUI.Interfaces;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameApplication;
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
		private Mock<IConsoleUI>? _mockUserInterface;
		private Mock<IGameLogic>? _mockGameLogic;

		[TestInitialize]
		public void SetUp()
		{
			// Initialize mocks for dependencies and game flow controller.
			_mockDependencyInitializer = new Mock<IDependencyInitializer>();
			_mockGameFlowController = new Mock<IGameFlowController>();
			_mockUserInterface = new Mock<IConsoleUI>();
			_mockGameLogic = new Mock<IGameLogic>();

			// Set up the dependency initializer mock to return mocked interfaces.
			_mockDependencyInitializer
				.Setup(di => di.InitializeDependencies())
				.Returns((_mockUserInterface.Object, _mockGameLogic.Object));

			// Configure Program to use mocks for testing.
			Program.DependencyInitializerFactory = () => _mockDependencyInitializer.Object;
			Program.GameFlowControllerFactory = () => _mockGameFlowController.Object;
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