using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.DependencyInjection.Implementations;
using Laboration.GameLogic.Implementations;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Implementations;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Implementations;
using Laboration.Validation.Interfaces;

namespace Laboration.UnitTests.DependencyInjection
{
	[TestClass]
	public class BullsAndCowsDependencyInitializerTests
	{
		private readonly BullsAndCowsDependencyInitializer _dependencyInitializer = new();
		private IConsoleUI? _consoleUI;
		private IGameLogic? _gameLogic;
		private IValidation? _validation;
		private IHighScoreManager? _highScoreManager;

		// Initializes dependencies before each test.
		[TestInitialize]
		public void TestInitialize()
		{
			_validation = _dependencyInitializer.CreateValidation();
			_highScoreManager = _dependencyInitializer.CreateHighScoreManager();

			(_consoleUI, _gameLogic) = _dependencyInitializer.InitializeDependencies();
		}

		// Tests if InitializeDependencies returns non-null instances of dependencies.
		[TestMethod]
		public void InitializeDependencies_ReturnsValidInstances()
		{
			// Assert
			Assert.IsNotNull(_consoleUI, "Expected a non-null ConsoleUI instance from InitializeDependencies.");
			Assert.IsNotNull(_gameLogic, "Expected a non-null GameLogic instance from InitializeDependencies.");
			Assert.IsNotNull(_validation, "Expected a non-null Validation instance.");
			Assert.IsNotNull(_highScoreManager, "Expected a non-null HighScoreManager instance.");
		}

		// Tests if InitializeDependencies returns instances that implement the correct interfaces.
		[TestMethod]
		public void InitializeDependencies_ReturnsCorrectInterfaces()
		{
			// Assert
			Assert.IsInstanceOfType(_consoleUI, typeof(IConsoleUI), "ConsoleUI instance should implement IConsoleUI.");
			Assert.IsInstanceOfType(_gameLogic, typeof(IGameLogic), "GameLogic instance should implement IGameLogic.");
			Assert.IsInstanceOfType(_validation, typeof(IValidation), "Validation instance should implement IValidation.");
			Assert.IsInstanceOfType(_highScoreManager, typeof(IHighScoreManager), "HighScoreManager instance should implement IHighScoreManager.");
		}

		// Tests if InitializeDependencies returns instances of the correct concrete types.
		[TestMethod]
		public void InitializeDependencies_UsesCorrectImplementations()
		{
			// Assert
			Assert.IsInstanceOfType(_consoleUI, typeof(BullsAndCowsConsoleUI), "ConsoleUI instance should be of type BullsAndCowsConsoleUI.");
			Assert.IsInstanceOfType(_gameLogic, typeof(BullsAndCowsGameLogic), "GameLogic instance should be of type BullsAndCowsGameLogic.");
			Assert.IsInstanceOfType(_validation, typeof(BullsAndCowsValidation), "Validation instance should be of type BullsAndCowsValidation.");
			Assert.IsInstanceOfType(_highScoreManager, typeof(BullsAndCowsHighScoreManager), "HighScoreManager instance should be of type BullsAndCowsHighScoreManager.");
		}

		// Test to verify that the GameType is correctly set to BullsAndCows
		[TestMethod]
		public void GameType_IsCorrectlySetToBullsAndCows()
		{
			// Assert
			Assert.AreEqual(GameTypes.BullsAndCows, _dependencyInitializer.GameType, "GameType should be BullsAndCows.");
		}
	}
}