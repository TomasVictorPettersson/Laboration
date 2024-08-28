using ConsoleUI.Implementations;
using ConsoleUI.Interfaces;
using GameLogic.Implementations;
using GameLogic.Interfaces;
using GameResources.Enums;
using HighScoreManagement.Implementations;
using HighScoreManagement.Interfaces;
using Validation.Implementations;
using Validation.Interfaces;

namespace DependencyInjection.Implementations
{
	// Concrete implementation for MasterMind game.
	public class MasterMindDependencyInitializer : GameDependencyInitializerBase
	{
		public MasterMindDependencyInitializer() : base(GameTypes.MasterMind)
		{
		}

		// Creates and returns the MasterMind specific game logic.
		public override IGameLogic CreateGameLogic(IConsoleUI consoleUI, IValidation validation, IHighScoreManager highScoreManager)
		{
			return new MasterMindGameLogic(highScoreManager, consoleUI, validation);
		}

		// Creates and returns the specific validation implementation for MasterMind.
		public override IValidation CreateValidation()
		{
			return new MasterMindValidation();
		}

		// Creates and returns the specific console UI implementation for MasterMind.
		public override IConsoleUI CreateConsoleUI(IValidation validation, IHighScoreManager highScoreManager)
		{
			return new MasterMindConsoleUI(validation, highScoreManager);
		}

		// Creates and returns the specific high score manager implementation for MasterMind.
		public override IHighScoreManager CreateHighScoreManager()
		{
			return new MasterMindHighScoreManager();
		}
	}
}