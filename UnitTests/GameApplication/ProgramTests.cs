using ConsoleUI.Interfaces;
using DependencyInjection.Interfaces;
using GameApplication.Implementations;
using GameFactory.Interfaces;
using GameFlow.Interfaces;
using GameLogic.Interfaces;
using GameResources.Enums;
using Moq;

namespace UnitTests.GameApplication
{
	[TestClass]
	public class ProgramTests
	{
		private readonly Mock<IGameSelection> _mockGameSelector = new();
		private readonly Mock<IGameFactoryCreator> _mockFactoryCreator = new();
		private readonly Mock<IGameFactory> _mockGameFactory = new();
		private readonly Mock<IDependencyInitializer> _mockDependencyInitializer = new();
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private readonly Mock<IGameLogic> _mockGameLogic = new();
		private readonly Mock<IGameFlowController> _mockGameFlowController = new();
		private Program _program = null!;

		[TestInitialize]
		public void Setup()
		{
			_mockGameFactory.Setup(gf => gf.CreateDependencyInitializer()).Returns(_mockDependencyInitializer.Object);
			_mockGameFactory.Setup(gf => gf.CreateGameFlowController()).Returns(_mockGameFlowController.Object);
			_mockDependencyInitializer.Setup(di => di.InitializeDependencies()).Returns((_mockConsoleUI.Object, _mockGameLogic.Object));
			_mockGameFlowController.Setup(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()));
			_mockFactoryCreator.Setup(fc => fc.CreateGameFactory(It.IsAny<GameTypes>())).Returns(_mockGameFactory.Object);
			_program = new Program(_mockGameSelector.Object, _mockFactoryCreator.Object);
		}

		// Verifies that the game loop terminates correctly when the Quit option is selected.
		[TestMethod]
		public void RunGameLoop_QuittingDoesNotContinueLoop()
		{
			// Arrange
			_mockGameSelector.SetupSequence(gs => gs.SelectGameType())
				.Returns(GameTypes.BullsAndCows) // First call returns BullsAndCows
				.Returns(GameTypes.Quit);         // Second call returns Quit

			// Act
			_program.RunGameLoop();

			// Assert
			_mockGameFlowController.Verify(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()), Times.Once, "Expected ExecuteGameLoop to be called once when quitting.");
		}

		// Verifies that the game flow controller is executed for each valid game type.
		[TestMethod]
		public void RunGameLoop_ExecutesGameFlowControllerForValidGameTypes()
		{
			// Arrange
			_mockGameSelector.SetupSequence(gs => gs.SelectGameType())
				.Returns(GameTypes.BullsAndCows) // First call
				.Returns(GameTypes.MasterMind)    // Second call
				.Returns(GameTypes.Quit);         // Third call

			// Act
			_program.RunGameLoop();

			// Assert
			_mockGameFlowController.Verify(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()), Times.Exactly(2), "Expected ExecuteGameLoop to be called twice for valid game types.");
		}

		// Verifies that InitializeGameFactory correctly returns true and sets the factory for a valid game type.
		[TestMethod]
		public void InitializeGameFactory_ValidGameType_ReturnsTrueAndSetsFactory()
		{
			// Act
			var result = _program.InitializeGameFactory(GameTypes.BullsAndCows);

			// Assert
			Assert.IsTrue(result, "Expected InitializeGameFactory to return true for a valid game type.");
			Assert.IsNotNull(_program.GetFactory(), "Expected _factory to be set for a valid game type.");
			Assert.IsInstanceOfType(_program.GetFactory(), typeof(IGameFactory), "Expected _factory to be an instance of IGameFactory.");
		}

		// Verifies that InitializeDependencies returns correctly initialized dependencies for a valid factory.
		[TestMethod]
		public void InitializeDependencies_ValidFactory_ReturnsInitializedDependencies()
		{
			// Arrange
			_program.InitializeGameFactory(GameTypes.BullsAndCows);

			// Act
			var (userInterface, gameLogic) = _program.InitializeDependencies();

			// Assert
			Assert.IsNotNull(userInterface, "Expected InitializeDependencies to return a non-null IConsoleUI.");
			Assert.IsInstanceOfType(userInterface, typeof(IConsoleUI), "Expected InitializeDependencies to return an instance of IConsoleUI.");
			Assert.IsNotNull(gameLogic, "Expected InitializeDependencies to return a non-null IGameLogic.");
			Assert.IsInstanceOfType(gameLogic, typeof(IGameLogic), "Expected InitializeDependencies to return an instance of IGameLogic.");
		}

		// Verifies that InitializeGameFactory correctly sets the factory for different game types, excluding Quit.
		[TestMethod]
		public void InitializeGameFactory_DifferentGameTypes_SuccessfullySetsFactory()
		{
			// Act & Assert
			foreach (var gameType in Enum.GetValues<GameTypes>())
			{
				if (gameType == GameTypes.Quit) continue; // Skip Quit type

				var result = _program.InitializeGameFactory(gameType);

				Assert.IsTrue(result, $"Expected InitializeGameFactory to return true for game type {gameType}.");
				Assert.IsNotNull(_program.GetFactory(), $"Expected _factory to be set for game type {gameType}.");
				Assert.IsInstanceOfType(_program.GetFactory(), typeof(IGameFactory), $"Expected _factory to be an instance of IGameFactory for game type {gameType}.");
			}
		}
	}
}