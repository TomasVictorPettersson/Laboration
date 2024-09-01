using ConsoleUI.Interfaces;
using GameResources.Constants;
using GameResources.Enums;
using HighScoreManagement.Interfaces;
using System.Text;
using Validation.Interfaces;

namespace GameLogic.Implementations
{
	// Represents the game logic specific to the MasterMind game.
	public class MasterMindGameLogic(IHighScoreManager highScoreManager, IConsoleUI consoleUI, IValidation validation) : GameLogicBase(highScoreManager, consoleUI, validation)
	{
		// Generates a random 4-digit secret number where digits can repeat.
		public override string MakeSecretNumber()
		{
			Random random = new();
			StringBuilder secretNumber = new();

			// Continue adding random digits until having a 4-digit number
			while (secretNumber.Length < GameConstants.SecretNumberLength)
			{
				int randomDigit = random.Next(GameConstants.DigitRange);
				secretNumber.Append(randomDigit);
			}

			return secretNumber.ToString();
		}

		// Returns the type of game (MasterMind).
		public override GameTypes GetGameType()
		{
			return GameTypes.MasterMind;
		}
	}
}