using Laboration.ConsoleUI.Implementations;
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
	// Concrete implementation for Bulls and Cows game.
	public class BullsAndCowsDependencyInitializer : GameDependencyInitializerBase
	{
		public BullsAndCowsDependencyInitializer() : base(GameTypes.BullsAndCows)
		{
		}

		// Creates and returns the Bulls and Cows specific game logic.
		protected override IGameLogic CreateGameLogic(IConsoleUI consoleUI, IValidation validation, IHighScoreManager highScoreManager)
		{
			return new BullsAndCowsGameLogic(highScoreManager, consoleUI, validation);
		}

		// Creates and returns the specific validation implementation for Bulls and Cows.
		protected override IValidation CreateValidation()
		{
			return new BullsAndCowsValidation(); // Specific validation for Bulls and Cows
		}

		// Creates and returns the specific console UI implementation for Bulls and Cows.
		protected override IConsoleUI CreateConsoleUI(IValidation validation, IHighScoreManager highScoreManager)
		{
			return new BullsAndCowsConsoleUI(validation, highScoreManager); // Specific UI for Bulls and Cows
		}

		// Creates and returns the specific high score manager implementation for Bulls and Cows.
		protected override IHighScoreManager CreateHighScoreManager()
		{
			return new BullsAndCowsHighScoreManager(); // Specific high score manager for Bulls and Cows
		}
	}
}