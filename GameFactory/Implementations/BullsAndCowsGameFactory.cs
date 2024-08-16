﻿using Laboration.DependencyInjection.Implementations;
using Laboration.DependencyInjection.Interfaces;
using Laboration.GameFactory.Interfaces;
using Laboration.GameFlow.Implementations;
using Laboration.GameFlow.Interfaces;
using Laboration.GameResources.Enums;

namespace Laboration.GameFactory.Implementations
{
	// Implementation of the IGameFactory interface for
	// creating game-related components for the Bulls and Cows game.
	public class BullsAndCowsGameFactory : IGameFactory
	{
		// Creates an instance of IDependencyInitializer to set up game dependencies.
		public IDependencyInitializer CreateDependencyInitializer()
		{
			return new GameDependencyInitializer(GameTypes.BullsAndCows);
		}

		// Creates an instance of IGameFlowController to manage the game flow.
		public IGameFlowController CreateGameFlowController()
		{
			return new GameFlowController(GameTypes.BullsAndCows);
		}
	}
}