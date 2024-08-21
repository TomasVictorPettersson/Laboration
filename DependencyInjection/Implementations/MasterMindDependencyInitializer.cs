﻿using Laboration.ConsoleUI.Implementations;
using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Implementations;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Implementations;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Implementations;
using Laboration.Validation.Interfaces;

namespace Laboration.DependencyInjection.Implementations
{
	// Concrete implementation for MasterMind game.
	public class MasterMindDependencyInitializer : GameDependencyInitializerBase
	{
		public MasterMindDependencyInitializer() : base(GameTypes.MasterMind)
		{
		}

		// Creates and returns the MasterMind specific game logic.
		protected override IGameLogic CreateGameLogic(IConsoleUI consoleUI, IValidation validation, IHighScoreManager highScoreManager)
		{
			return new MasterMindGameLogic(highScoreManager, consoleUI, validation);
		}

		// Creates and returns the specific validation implementation for MasterMind.
		protected override IValidation CreateValidation()
		{
			return new MasterMindValidation(); // Specific validation for MasterMind
		}

		// Creates and returns the specific console UI implementation for MasterMind.
		protected override IConsoleUI CreateConsoleUI(IValidation validation, IHighScoreManager highScoreManager)
		{
			return new MasterMindConsoleUI(validation, highScoreManager); // Specific UI for MasterMind
		}

		// Creates and returns the specific high score manager implementation for MasterMind.
		protected override IHighScoreManager CreateHighScoreManager()
		{
			return new MasterMindHighScoreManager(); // Specific high score manager for MasterMind
		}
	}
}