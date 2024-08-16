using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.DependencyInjection.Implementations;
using Laboration.GameLogic.Implementations;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.UnitTests.DependencyInjection
{
	[TestClass]
	public class GameDependencyInitializerTests
	{
		private readonly GameDependencyInitializer _dependencyInitializer = new(GameTypes.BullsAndCows);
		private IConsoleUI? _consoleUI;
		private IGameLogic? _gameLogic;

		// Initializes dependencies before each test.
		[TestInitialize]
		public void TestInitialize()
		{
			// Initializes the dependencies and assigns them to fields.
			(_consoleUI, _gameLogic) = _dependencyInitializer.InitializeDependencies();
		}

		// Tests if InitializeDependencies returns non-null instances of dependencies.
		[TestMethod]
		public void InitializeDependencies_ReturnsValidInstances()
		{
			// Assert
			Assert.IsNotNull(_consoleUI, "Expected a non-null ConsoleUI instance from InitializeDependencies.");
			Assert.IsNotNull(_gameLogic, "Expected a non-null GameLogic instance from InitializeDependencies.");
		}

		// Tests if InitializeDependencies returns instances that implement the correct interfaces.
		[TestMethod]
		public void InitializeDependencies_ReturnsCorrectInterfaces()
		{
			// Assert
			Assert.IsInstanceOfType(_consoleUI, typeof(IConsoleUI), "ConsoleUI instance should implement IConsoleUI.");
			Assert.IsInstanceOfType(_gameLogic, typeof(IGameLogic), "GameLogic instance should implement IGameLogic.");
		}

		// Tests if InitializeDependencies returns instances of the correct concrete types.
		[TestMethod]
		public void InitializeDependencies_UsesCorrectImplementations()
		{
			// Assert
			Assert.IsInstanceOfType(_consoleUI, typeof(GameConsoleUI), "ConsoleUI instance should be of type GameConsoleUI.");
			Assert.IsInstanceOfType(_gameLogic, typeof(BullsAndCowsGameLogic), "GameLogic instance should be of type BullsAndCowsGameLogic.");
		}
	}
}