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
	// Concrete implementation for Bulls and Cows game.
	public class BullsAndCowsDependencyInitializer : DependencyInitializerBase
	{
		public BullsAndCowsDependencyInitializer() : base(GameTypes.BullsAndCows)
		{
		}

		// Creates and returns the Bulls and Cows specific game logic.
		public override IGameLogic CreateGameLogic(IConsoleUI consoleUI, IValidation validation, IHighScoreManager highScoreManager)
		{
			return new BullsAndCowsGameLogic(highScoreManager, consoleUI, validation);
		}

		// Creates and returns the specific validation implementation for Bulls and Cows.
		public override IValidation CreateValidation()
		{
			return new BullsAndCowsValidation();
		}

		// Creates and returns the specific console UI implementation for Bulls and Cows.
		public override IConsoleUI CreateConsoleUI(IValidation validation, IHighScoreManager highScoreManager)
		{
			return new BullsAndCowsConsoleUI(validation, highScoreManager);
		}

		// Creates and returns the specific high score manager implementation for Bulls and Cows.
		public override IHighScoreManager CreateHighScoreManager()
		{
			return new BullsAndCowsHighScoreManager();
		}
	}
}