using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Implementations;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.UnitTests.GameFactory
{
	[TestClass]
	public class MasterMindFactoryTests
	{
		private readonly MasterMindFactory _factory = new();

		// Verifies that CreateDependencyInitializer returns an instance of MasterMindDependencyInitializer.
		[TestMethod]
		public void CreateDependencyInitializer_ReturnsMasterMindDependencyInitializer()
		{
			// Act
			IDependencyInitializer dependencyInitializer = _factory.CreateDependencyInitializer();

			// Assert
			Assert.IsInstanceOfType(
				dependencyInitializer,
				typeof(MasterMindDependencyInitializer),
				"Expected an instance of MasterMindDependencyInitializer."
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

		// Verifies that CreateGameFlowController returns an instance of MasterMindGameFlowController.
		[TestMethod]
		public void CreateGameFlowController_ReturnsMasterMindGameFlowController()
		{
			// Act
			IGameFlowController gameFlowController = _factory.CreateGameFlowController();

			// Assert
			Assert.IsInstanceOfType(
				gameFlowController,
				typeof(MasterMindGameFlowController),
				"Expected an instance of MasterMindGameFlowController."
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

		// Verifies that the GameType is correctly set to MasterMind.
		[TestMethod]
		public void Factory_ShouldHaveCorrectGameType()
		{
			// Assert
			Assert.AreEqual(
				GameTypes.MasterMind,
				_factory.GameType,
				"GameType should be set to MasterMind."
			);
		}
	}
}