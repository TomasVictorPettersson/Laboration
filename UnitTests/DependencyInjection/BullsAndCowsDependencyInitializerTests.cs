using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.DependencyInjection.Implementations;
using Laboration.GameLogic.Implementations;
using Laboration.GameLogic.Interfaces;

namespace Laboration.UnitTests.DependencyInjection
{
	[TestClass]
	public class BullsAndCowsDependencyInitializerTests
	{
		private readonly BullsAndCowsDependencyInitializer _dependencyInitializer = new();
		private IConsoleUI? _consoleUI;
		private IGameLogic? _gameLogic;

		[TestInitialize]
		public void TestInitialize()
		{
			(_consoleUI, _gameLogic) = _dependencyInitializer.InitializeDependencies();
		}

		[TestMethod]
		public void InitializeDependencies_ReturnsValidInstances()
		{
			// Assert
			Assert.IsNotNull(_consoleUI, "Expected a non-null ConsoleUI instance from InitializeDependencies.");
			Assert.IsNotNull(_gameLogic, "Expected a non-null GameLogic instance from InitializeDependencies.");
		}

		[TestMethod]
		public void InitializeDependencies_ReturnsCorrectInterfaces()
		{
			// Assert
			Assert.IsInstanceOfType(_consoleUI, typeof(IConsoleUI), "ConsoleUI instance should implement IConsoleUI.");
			Assert.IsInstanceOfType(_gameLogic, typeof(IGameLogic), "GameLogic instance should implement IGameLogic.");
		}

		[TestMethod]
		public void InitializeDependencies_UsesCorrectImplementations()
		{
			// Assert
			Assert.IsInstanceOfType(_consoleUI, typeof(BullsAndCowsConsoleUI), "ConsoleUI instance should be of type BullsAndCowsConsoleUI.");
			Assert.IsInstanceOfType(_gameLogic, typeof(BullsAndCowsGameLogic), "GameLogic instance should be of type BullsAndCowsGameLogic.");
		}
	}
}