﻿using DependencyInjection.Implementations;
using DependencyInjection.Interfaces;
using GameFactory.Implementations;
using GameFlow.Implementations;
using GameFlow.Interfaces;
using GameResources.Enums;

namespace UnitTests.GameFactory
{
	[TestClass]
	public class BullsAndCowsFactoryTests
	{
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

		// Verifies that CreateGameFlowController returns an instance of GameFlowController.
		[TestMethod]
		public void CreateGameFlowController_ReturnsGameFlowController()
		{
			// Act
			IGameFlowController gameFlowController = _factory.CreateGameFlowController();

			// Assert
			Assert.IsInstanceOfType(
				gameFlowController,
				typeof(GameFlowController),
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