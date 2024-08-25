using Laboration.ConsoleUI.Interfaces;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameApplication;
using Laboration.GameFactory.Interfaces;
using Laboration.GameFlow.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Enums;
using Moq;

namespace Laboration.UnitTests.GameApplication
{
	[TestClass]
	public class ProgramTests
	{
		private readonly Mock<IGameSelector> _mockGameSelector = new();
		private readonly Mock<IFactoryCreator> _mockFactoryCreator = new();
		private readonly Mock<IGameFactory> _mockGameFactory = new();
		private readonly Mock<IDependencyInitializer> _mockDependencyInitializer = new();
		private readonly Mock<IConsoleUI> _mockConsoleUI = new();
		private readonly Mock<IGameLogic> _mockGameLogic = new();
		private readonly Mock<IGameFlowController> _mockGameFlowController = new();

		[TestInitialize]
		public void Setup()
		{
			_mockGameFactory.Setup(gf => gf.CreateDependencyInitializer()).Returns(_mockDependencyInitializer.Object);
			_mockGameFactory.Setup(gf => gf.CreateGameFlowController()).Returns(_mockGameFlowController.Object);
			_mockDependencyInitializer.Setup(di => di.InitializeDependencies()).Returns((_mockConsoleUI.Object, _mockGameLogic.Object));
			_mockGameFlowController.Setup(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()));

			_mockFactoryCreator.Setup(fc => fc.CreateFactory(It.IsAny<GameTypes>())).Returns(_mockGameFactory.Object);
		}

		// Verifies that when the game selector returns a quit option, the game loop does not continue.
		[TestMethod]
		public void RunGameLoop_QuittingDoesNotContinueLoop()
		{
			// Arrange
			_mockGameSelector.SetupSequence(gs => gs.SelectGameType())
				.Returns(GameTypes.BullsAndCows) // First call returns BullsAndCows
				.Returns(GameTypes.Quit);         // Second call returns Quit

			var program = new Program(_mockGameSelector.Object, _mockFactoryCreator.Object);

			// Act
			program.RunGameLoop();

			// Assert
			_mockGameFlowController.Verify(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()), Times.Once, "Expected ExecuteGameLoop to be called once when quitting.");
		}

		// Ensures that the game flow controller is executed for each valid game type and not for quit.
		[TestMethod]
		public void RunGameLoop_ExecutesGameFlowControllerForValidGameTypes()
		{
			// Arrange
			_mockGameSelector.SetupSequence(gs => gs.SelectGameType())
				.Returns(GameTypes.BullsAndCows) // First call
				.Returns(GameTypes.MasterMind)    // Second call
				.Returns(GameTypes.Quit);         // Third call

			var program = new Program(_mockGameSelector.Object, _mockFactoryCreator.Object);

			// Act
			program.RunGameLoop();

			// Assert
			_mockGameFlowController.Verify(gfc => gfc.ExecuteGameLoop(It.IsAny<IConsoleUI>(), It.IsAny<IGameLogic>()), Times.Exactly(2), "Expected ExecuteGameLoop to be called twice for valid game types.");
		}

		// Verifies that CreateAndInitializeProgram correctly creates and returns a Program instance.
		[TestMethod]
		public void CreateAndInitializeProgram_ValidInputs_ReturnsProgramInstance()
		{
			// Act
			var program = Program.CreateAndInitializeProgram();

			// Assert
			Assert.IsNotNull(program, "Expected CreateAndInitializeProgram to return a non-null Program instance.");
			Assert.IsInstanceOfType(program, typeof(Program), "Expected CreateAndInitializeProgram to return an instance of Program.");
		}

		// Checks that InitializeGameFactory returns true and sets the factory correctly for a valid game type.
		[TestMethod]
		public void InitializeGameFactory_ValidGameType_ReturnsTrueAndSetsFactory()
		{
			// Arrange
			var program = new Program(_mockGameSelector.Object, _mockFactoryCreator.Object);

			// Act
			var result = program.InitializeGameFactory(GameTypes.BullsAndCows);

			// Assert
			Assert.IsTrue(result, "Expected InitializeGameFactory to return true for a valid game type.");
			Assert.IsNotNull(program.GetFactory(), "Expected _factory to be set for a valid game type.");
			Assert.IsInstanceOfType(program.GetFactory(), typeof(IGameFactory), "Expected _factory to be an instance of IGameFactory.");
		}

		// Verifies that InitializeDependencies returns non-null and correctly typed dependencies.
		[TestMethod]
		public void InitializeDependencies_ValidFactory_ReturnsInitializedDependencies()
		{
			// Arrange
			var program = new Program(_mockGameSelector.Object, _mockFactoryCreator.Object);
			program.InitializeGameFactory(GameTypes.BullsAndCows);

			// Act
			var (userInterface, gameLogic) = program.InitializeDependencies();

			// Assert
			Assert.IsNotNull(userInterface, "Expected InitializeDependencies to return a non-null IConsoleUI.");
			Assert.IsInstanceOfType(userInterface, typeof(IConsoleUI), "Expected InitializeDependencies to return an instance of IConsoleUI.");
			Assert.IsNotNull(gameLogic, "Expected InitializeDependencies to return a non-null IGameLogic.");
			Assert.IsInstanceOfType(gameLogic, typeof(IGameLogic), "Expected InitializeDependencies to return an instance of IGameLogic.");
		}

		// Tests that InitializeGameFactory correctly sets the factory for different game types, excluding Quit.
		[TestMethod]
		public void InitializeGameFactory_DifferentGameTypes_SuccessfullySetsFactory()
		{
			// Arrange
			var program = new Program(_mockGameSelector.Object, _mockFactoryCreator.Object);

			foreach (var gameType in Enum.GetValues<GameTypes>())
			{
				if (gameType == GameTypes.Quit) continue; // Skip Quit type

				// Act
				var result = program.InitializeGameFactory(gameType);

				// Assert
				Assert.IsTrue(result, $"Expected InitializeGameFactory to return true for game type {gameType}.");
				Assert.IsNotNull(program.GetFactory(), $"Expected _factory to be set for game type {gameType}.");
				Assert.IsInstanceOfType(program.GetFactory(), typeof(IGameFactory), $"Expected _factory to be an instance of IGameFactory for game type {gameType}.");
			}
		}
	}
}