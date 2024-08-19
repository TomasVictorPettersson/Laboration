using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Implementations;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.UnitTests.GameFactory
{
	[TestClass]
	public class GameComponentFactoryTests
	{
		// Instance of the game factory to be tested.
		private readonly GameComponentFactory _factory = new(GameTypes.BullsAndCows);

		// Verifies that CreateDependencyInitializer returns an instance of GameDependencyInitializer.
		[TestMethod]
		public void CreateDependencyInitializer_ReturnsBullsAndCowsDependencyInitializer()
		{
			// Act
			IDependencyInitializer dependencyInitializer = _factory.CreateDependencyInitializer();

			// Assert
			Assert.IsInstanceOfType(
				dependencyInitializer,
				typeof(GameDependencyInitializer),
				"Expected an instance of GameDependencyInitializer."
			);
		}

		// Ensures that CreateDependencyInitializer does not return null.
		[TestMethod]
		public void CreateDependencyInitializer_ReturnsNonNullDependencyInitializer()
		{
			// Act
			IDependencyInitializer dependencyInitializer = _factory.CreateDependencyInitializer();

			// Assert
			Assert.IsNotNull(
				dependencyInitializer,
				"DependencyInitializer should not be null."
			);
		}

		// Verifies that CreateGameFlowController returns an instance of GameFlowController.
		[TestMethod]
		public void CreateGameFlowController_ReturnsBullsAndCowsGameFlowController()
		{
			// Act
			IGameFlowController gameFlowController = _factory.CreateGameFlowController();

			// Assert
			Assert.IsInstanceOfType(
				gameFlowController,
				typeof(GameFlowController),
				"Expected an instance of GameFlowController."
			);
		}

		// Ensures that CreateGameFlowController does not return null.
		[TestMethod]
		public void CreateGameFlowController_ReturnsNonNullGameFlowController()
		{
			// Act
			IGameFlowController gameFlowController = _factory.CreateGameFlowController();

			// Assert
			Assert.IsNotNull(
				gameFlowController,
				"GameFlowController should not be null."
			);
		}
	}
}