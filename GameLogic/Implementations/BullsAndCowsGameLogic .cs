using Laboration.ConsoleUI.Interfaces;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using System.Text;

namespace Laboration.GameLogic.Implementations
{
	// Manages the Bulls and Cows game logic, including setup, gameplay, and result handling.
	public class BullsAndCowsGameLogic : GameLogicBase
	{
		public BullsAndCowsGameLogic(IHighScoreManager highScoreManager, IConsoleUI consoleUI, IValidation validation)
			: base(highScoreManager, consoleUI, validation)
		{
		}

		// Generates a random 4-digit secret number with unique digits.
		public override string MakeSecretNumber()
		{
			Random randomGenerator = new();
			StringBuilder secretNumber = new();
			HashSet<int> usedDigits = new();

			while (secretNumber.Length < 4)
			{
				int randomDigit = randomGenerator.Next(10);
				if (usedDigits.Add(randomDigit))
				{
					secretNumber.Append(randomDigit);
				}
			}

			return secretNumber.ToString();
		}

		// Generates feedback for Bulls and Cows, i.e., 'BBBB,CCCC'.
		protected override string GenerateFeedback(string secretNumber, string guess)
		{
			int bulls = CountBulls(secretNumber, guess);
			int cows = CountCows(secretNumber, guess);
			return $"{new string('B', bulls)},{new string('C', cows)}";
		}

		// Counts the number of bulls in the guess compared to the secret number.
		private static int CountBulls(string secretNumber, string guess)
		{
			int bulls = 0;
			for (int i = 0; i < 4; i++)
			{
				if (secretNumber[i] == guess[i])
				{
					bulls++;
				}
			}
			return bulls;
		}

		// Counts the number of cows in the guess compared to the secret number.
		private static int CountCows(string secretNumber, string guess)
		{
			int cows = 0;
			Dictionary<char, int> digitFrequency = new();

			foreach (char digit in secretNumber)
			{
				if (digitFrequency.TryGetValue(digit, out int value))
				{
					digitFrequency[digit] = ++value;
				}
				else
				{
					digitFrequency[digit] = 1;
				}
			}

			for (int i = 0; i < 4; i++)
			{
				if (secretNumber[i] != guess[i] && digitFrequency.TryGetValue(guess[i], out int value) && value > 0)
				{
					cows++;
					digitFrequency[guess[i]] = --value;
				}
			}
			return cows;
		}

		// Returns the game type.
		protected override GameTypes GetGameType()
		{
			return GameTypes.BullsAndCows;
		}
	}
}