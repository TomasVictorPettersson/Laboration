using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Implementations;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;

namespace Laboration.UnitTests.GameFactory
{
	[TestClass]
	public class BullsAndCowsGameFactoryTests
	{
		private readonly BullsAndCowsGameFactory _factory = new();

		[TestMethod]
		public void CreateDependencyInitializer_ReturnsBullsAndCowsDependencyInitializer()
		{
			// Act
			IDependencyInitializer dependencyInitializer = _factory.CreateDependencyInitializer();

			// Assert
			Assert.IsInstanceOfType(dependencyInitializer, typeof(BullsAndCowsDependencyInitializer), "Expected an instance of BullsAndCowsDependencyInitializer.");
		}

		[TestMethod]
		public void CreateDependencyInitializer_ReturnsNonNullDependencyInitializer()
		{
			// Act
			IDependencyInitializer dependencyInitializer = _factory.CreateDependencyInitializer();

			// Assert
			Assert.IsNotNull(dependencyInitializer, "DependencyInitializer should not be null.");
		}

		[TestMethod]
		public void CreateGameFlowController_ReturnsBullsAndCowsGameFlowController()
		{
			// Act
			IGameFlowController gameFlowController = _factory.CreateGameFlowController();

			// Assert
			Assert.IsInstanceOfType(gameFlowController, typeof(BullsAndCowsGameFlowController), "Expected an instance of BullsAndCowsGameFlowController.");
		}

		[TestMethod]
		public void CreateGameFlowController_ReturnsNonNullGameFlowController()
		{
			// Act
			IGameFlowController gameFlowController = _factory.CreateGameFlowController();

			// Assert
			Assert.IsNotNull(gameFlowController, "GameFlowController should not be null.");
		}
	}
}