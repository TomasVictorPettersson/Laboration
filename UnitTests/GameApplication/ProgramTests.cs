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
		private readonly Mock<IDependencyInitializer> _mockDependencyInitializer = new();
		private readonly Mock<IGameFlowController> _mockGameFlowController = new();
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private readonly Mock<IGameLogic> _mockGameLogic = new();
		private readonly Mock<IGameFactory> _mockGameFactory = new();

		private readonly TextWriter _originalConsoleOut = Console.Out;
		private readonly StringWriter _consoleOutput = new();

		[TestInitialize]
		public void SetUp()
		{
			// Set up console output redirection.
			Console.SetOut(_consoleOutput);

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
			_mockDependencyInitializer.Verify(di => di.InitializeDependencies(), Times.Once, "Dependencies should be initialized once.");
		}

		[TestMethod]
		public void Main_ExecutesGameLoop()
		{
			// Act: Call the Main method to verify game loop execution.
			Program.Main();

			// Assert: Verify that ExecuteGameLoop was called once.
			_mockGameFlowController.Verify(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()), Times.Once, "Game loop should be executed once.");
		}

		[TestMethod]
		public void Main_HandlesExceptions()
		{
			// Arrange: Configure mock to throw exception.
			_mockGameFlowController
				.Setup(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()))
				.Throws(new Exception("Test exception"));

			// Act: Call the Main method to verify exception handling.
			Program.Main();

			// Assert: Verify that the error message was displayed correctly.
			Assert.IsTrue(_consoleOutput.ToString().Contains("An error occurred:"), "Error message was not displayed correctly.");
		}

		[TestCleanup]
		public void Cleanup()
		{
			// Restore the original console output.
			Console.SetOut(_originalConsoleOut);
			_consoleOutput.Dispose();
		}
	}
}