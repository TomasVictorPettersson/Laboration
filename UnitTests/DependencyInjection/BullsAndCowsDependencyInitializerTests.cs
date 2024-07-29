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
		[TestMethod]
		public void InitializeDependencies_ReturnsValidInstances()
		{
			// Arrange
			var dependencyInitializer = new BullsAndCowsDependencyInitializer();

			// Act
			var (consoleUI, gameLogic) = dependencyInitializer.InitializeDependencies();

			// Assert
			Assert.IsNotNull(consoleUI, "Expected a non-null ConsoleUI instance from InitializeDependencies.");
			Assert.IsNotNull(gameLogic, "Expected a non-null GameLogic instance from InitializeDependencies.");
		}

		[TestMethod]
		public void InitializeDependencies_ReturnsCorrectInterfaces()
		{
			// Arrange
			var dependencyInitializer = new BullsAndCowsDependencyInitializer();

			// Act
			var (consoleUI, gameLogic) = dependencyInitializer.InitializeDependencies();

			// Assert
			Assert.IsInstanceOfType(consoleUI, typeof(IConsoleUI), "ConsoleUI instance should implement IConsoleUI.");
			Assert.IsInstanceOfType(gameLogic, typeof(IGameLogic), "GameLogic instance should implement IGameLogic.");
		}

		[TestMethod]
		public void InitializeDependencies_UsesCorrectImplementations()
		{
			// Arrange
			var dependencyInitializer = new BullsAndCowsDependencyInitializer();

			// Act
			var (consoleUI, gameLogic) = dependencyInitializer.InitializeDependencies();

			// Assert
			Assert.IsInstanceOfType(consoleUI, typeof(BullsAndCowsConsoleUI), "ConsoleUI instance should be of type BullsAndCowsConsoleUI.");
			Assert.IsInstanceOfType(gameLogic, typeof(BullsAndCowsGameLogic), "GameLogic instance should be of type BullsAndCowsGameLogic.");
		}
	}
}