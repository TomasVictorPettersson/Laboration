using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;

namespace Laboration.ConsoleUI.Implementations
{
	// MasterMindConsoleUI handles specific UI interactions for the MasterMind game.
	// Inherits from ConsoleUIBase and implements game-specific methods for welcome messages,
	// goodbye messages, and input validation feedback.
	public class MasterMindConsoleUI(IValidation validation, IHighScoreManager highScoreManager) : ConsoleUIBase(validation, highScoreManager)
	{
		// Provides the welcome message format specific to the MasterMind game.
		// Overrides the base class implementation to return the MasterMind welcome message format.
		public override string GetWelcomeMessageFormat(GameTypes gameType)
		{
			return GameMessages.MasterMindWelcomeMessageFormat;
		}

		// Provides the goodbye message format specific to the MasterMind game.
		// Overrides the base class implementation to return the MasterMind goodbye message format.
		public override string GetGoodbyeMessageFormat(GameTypes gameType)
		{
			return GameMessages.MasterMindGoodbyeMessageFormat;
		}

		// Displays an invalid input message specifically for the MasterMind game.
		// Overrides the base class method to show the MasterMind-specific invalid input message.
		public override void DisplayInvalidInputMessage()
		{
			Console.WriteLine(UserInteractionMessages.MasterMindInvalidInputMessage);
		}
	}
}