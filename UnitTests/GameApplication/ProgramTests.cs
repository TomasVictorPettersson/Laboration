using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Laboration.ConsoleUI.Interfaces;
using Laboration.GameApplication;
using Laboration.GameFactory.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Enums;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Creators;
using Laboration.GameFlow.Interfaces;

namespace Laboration.UnitTests.GameApplication
{
	[TestClass]
	public class ProgramTests
	{
		private readonly Mock<IGameFlowController> _mockGameFlowController = new();
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private readonly Mock<IGameLogic> _mockGameLogic = new();
		private readonly Mock<IGameFactory> _mockGameFactory = new();
		private readonly Mock<IDependencyInitializer> _mockDependencyInitializer = new();
		private readonly Mock<IGameSelector> _mockGameSelector = new();

		private readonly TextWriter _originalConsoleOut = Console.Out;
		private readonly StringWriter _consoleOutput = new();

		[TestInitialize]
		public void SetUp()
		{
			Console.SetOut(_consoleOutput);

			// Setting up mocks
			_mockDependencyInitializer
				.Setup(di => di.InitializeDependencies())
				.Returns((_mockConsoleUI.Object, _mockGameLogic.Object));

			_mockGameFactory
				.Setup(factory => factory.CreateDependencyInitializer())
				.Returns(_mockDependencyInitializer.Object);
			_mockGameFactory
				.Setup(factory => factory.CreateGameFlowController())
				.Returns(_mockGameFlowController.Object);

			// Mocking FactoryCreator.CreateFactory
			Mock<IGameFactory> mockGameFactory = new();
			FactoryCreator.CreateFactory gameType => mockGameFactory.Object;

			// Setting Program's factory and game selector to use mocks
			Program.GameSelector = _mockGameSelector.Object;
		}

		[TestMethod]
		public void Main_ShouldInitializeDependencies()
		{
			// Arrange
			_mockGameSelector.Setup(gs => gs.SelectGameType()).Returns(GameTypes.Quit);
			_mockDependencyInitializer.Setup(di => di.InitializeDependencies())
				.Returns((_mockConsoleUI.Object, _mockGameLogic.Object));

			// Act
			Program.Main();

			// Assert
			_mockDependencyInitializer.Verify(
				di => di.InitializeDependencies(),
				Times.Once,
				"InitializeDependencies should be called once."
			);
		}

		[TestMethod]
		public void Main_ShouldExecuteGameLoop()
		{
			// Arrange
			_mockGameSelector.Setup(gs => gs.SelectGameType()).Returns(GameTypes.BullsAndCows);

			// Act
			Program.Main();

			// Assert
			_mockGameFlowController.Verify(
				gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()),
				Times.Once,
				"ExecuteGameLoop should be called once."
			);
		}

		[TestMethod]
		public void Main_ShouldHandleExceptions()
		{
			// Arrange
			_mockGameSelector.Setup(gs => gs.SelectGameType()).Returns(GameTypes.BullsAndCows);
			_mockGameFlowController
				.Setup(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()))
				.Throws(new Exception("Test exception"));

			// Act
			Program.Main();

			// Assert
			Assert.IsTrue(
				_consoleOutput.ToString().Contains("An unexpected error occurred:"),
				"Error message should be displayed on exception."
			);
		}

		[TestCleanup]
		public void Cleanup()
		{
			Console.SetOut(_originalConsoleOut);
			_consoleOutput.Dispose();
		}
	}
}