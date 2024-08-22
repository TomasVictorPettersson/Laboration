using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Implementations;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.UnitTests.GameFactory
{
	[TestClass]
	public class BullsAndCowsFactoryTests
	{
		// Instance of the game factory to be tested.
		private readonly BullsAndCowsFactory _factory = new();

		// Verifies that CreateDependencyInitializer returns an instance of BullsAndCowsDependencyInitializer.
		[TestMethod]
		public void CreateDependencyInitializer_ReturnsBullsAndCowsDependencyInitializer()
		{
			// Act
			IDependencyInitializer dependencyInitializer = _factory.CreateDependencyInitializer();

			// Assert
			Assert.IsInstanceOfType(
				dependencyInitializer,
				typeof(BullsAndCowsDependencyInitializer),
				"Expected an instance of BullsAndCowsDependencyInitializer."
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

		// Verifies that CreateGameFlowController returns an instance of BullsAndCowsGameFlowController.
		[TestMethod]
		public void CreateGameFlowController_ReturnsBullsAndCowsGameFlowController()
		{
			// Act
			IGameFlowController gameFlowController = _factory.CreateGameFlowController();

			// Assert
			Assert.IsInstanceOfType(
				gameFlowController,
				typeof(BullsAndCowsGameFlowController),
				"Expected an instance of BullsAndCowsGameFlowController."
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

		// Verifies that the GameType is correctly set to BullsAndCows.
		[TestMethod]
		public void Factory_ShouldHaveCorrectGameType()
		{
			// Assert
			Assert.AreEqual(
				GameTypes.BullsAndCows,
				_factory.GameType,
				"GameType should be set to BullsAndCows."
			);
		}
	}
}