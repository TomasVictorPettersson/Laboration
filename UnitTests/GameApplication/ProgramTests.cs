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
		// Mocks for dependencies used in tests.
		private readonly Mock<IDependencyInitializer> _mockDependencyInitializer = new();

		private readonly Mock<IGameFlowController> _mockGameFlowController = new();
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private readonly Mock<IGameLogic> _mockGameLogic = new();
		private readonly Mock<IGameFactory> _mockGameFactory = new();

		// Captures console output during tests.
		private readonly TextWriter _originalConsoleOut = Console.Out;

		private readonly StringWriter _consoleOutput = new();

		[TestInitialize]
		public void SetUp()
		{
			// Redirect console output to capture it.
			Console.SetOut(_consoleOutput);

			// Configure mocks to return predefined objects.
			_mockDependencyInitializer
				.Setup(di => di.InitializeDependencies())
				.Returns((_mockConsoleUI.Object, _mockGameLogic.Object));

			_mockGameFactory
				.Setup(factory => factory.CreateDependencyInitializer())
				.Returns(_mockDependencyInitializer.Object);
			_mockGameFactory
				.Setup(factory => factory.CreateGameFlowController())
				.Returns(_mockGameFlowController.Object);

			// Set Program's factory to use the mock game factory.
			Program.Factory = _mockGameFactory.Object;
		}

		// Verifies that the Main method calls InitializeDependencies once.
		[TestMethod]
		public void Main_ShouldInitializeDependencies()
		{
			// Act
			Program.Main();

			// Assert
			_mockDependencyInitializer.Verify(
				di => di.InitializeDependencies(),
				Times.Once,
				"InitializeDependencies should be called once."
			);
		}

		// Ensures that the Main method calls ExecuteGameLoop once.
		[TestMethod]
		public void Main_ShouldExecuteGameLoop()
		{
			// Act
			Program.Main();

			// Assert
			_mockGameFlowController.Verify(
				gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()),
				Times.Once,
				"ExecuteGameLoop should be called once."
			);
		}

		// Checks that exceptions are handled and an error message is displayed correctly.
		[TestMethod]
		public void Main_ShouldHandleExceptions()
		{
			// Arrange
			_mockGameFlowController
				.Setup(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()))
				.Throws(new Exception("Test exception"));

			// Act
			Program.Main();

			// Assert
			Assert.IsTrue(
				_consoleOutput.ToString().Contains("An error occurred:"),
				"Error message should be displayed on exception."
			);
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