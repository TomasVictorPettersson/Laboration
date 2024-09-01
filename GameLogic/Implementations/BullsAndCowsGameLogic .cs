using ConsoleUI.Interfaces;
using GameResources.Enums;
using HighScoreManagement.Interfaces;
using System.Text;
using Validation.Interfaces;

namespace GameLogic.Implementations
{
	// Represents the game logic specific to the Bulls and Cows game.
	public class BullsAndCowsGameLogic(IHighScoreManager highScoreManager, IConsoleUI consoleUI, IValidation validation) : GameLogicBase(highScoreManager, consoleUI, validation)
	{
		// Generates a random 4-digit secret number where each digit is unique.
		public override string MakeSecretNumber()
		{
			Random random = new();
			StringBuilder secretNumber = new();
			HashSet<int> usedDigits = [];

			// Continue adding unique random digits until having a 4-digit number
			while (secretNumber.Length < 4)
			{
				int randomDigit = random.Next(10);
				if (usedDigits.Add(randomDigit))
				{
					secretNumber.Append(randomDigit);
				}
			}

			return secretNumber.ToString();
		}

		// Returns the type of game (Bulls and Cows).
		public override GameTypes GetGameType()
		{
			return GameTypes.BullsAndCows;
		}
	}
}