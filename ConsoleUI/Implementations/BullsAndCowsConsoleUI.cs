using GameResources.Constants;
using GameResources.Enums;
using HighScoreManagement.Interfaces;
using Validation.Interfaces;

namespace ConsoleUI.Implementations
{
	// BullsAndCowsConsoleUI handles specific UI interactions for the Bulls and Cows game.
	// It inherits from ConsoleUIBase, implementing game-specific logic for welcome messages,
	// goodbye messages, and input validation feedback.
	public class BullsAndCowsConsoleUI(IValidation validation, IHighScoreManager highScoreManager) : ConsoleUIBase(validation, highScoreManager)
	{
		// Provides the welcome message format specific to the Bulls and Cows game.
		// Overrides the base class implementation to return the Bulls and Cows welcome message format.
		public override string GetWelcomeMessageFormat(GameTypes gameType)
		{
			return GameMessages.BullsAndCowsWelcomeMessageFormat;
		}

		// Provides the goodbye message format specific to the Bulls and Cows game.
		// Overrides the base class implementation to return the Bulls and Cows goodbye message format.
		public override string GetGoodbyeMessageFormat(GameTypes gameType)
		{
			return GameMessages.BullsAndCowsGoodbyeMessageFormat;
		}

		// Displays an invalid input message specifically for the Bulls and Cows game.
		// Overrides the base class method to show the Bulls and Cows specific invalid input message.
		public override void DisplayInvalidInputMessage()
		{
			Console.WriteLine(UserInteractionMessages.BullsAndCowsInvalidInputMessage);
		}
	}
}