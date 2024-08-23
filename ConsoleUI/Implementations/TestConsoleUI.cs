using Laboration.ConsoleUI.Implementations;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;

namespace Laboration.ConsoleUI.Implementations
{
	// Derived class for testing abstract methods
	public class TestConsoleUI(IValidation validation, IHighScoreManager highScoreManager) : ConsoleUIBase(validation, highScoreManager)
	{
		public override string GetWelcomeMessageFormat(GameTypes gameType) => "Welcome {0}!";

		public override string GetGoodbyeMessageFormat(GameTypes gameType) => "Goodbye {0}!";

		public override void DisplayInvalidInputMessage() => Console.WriteLine("Invalid input. Please try again.");
	}
}