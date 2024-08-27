using Laboration.ConsoleUI.Interfaces;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using System.Text;

namespace Laboration.GameLogic.Implementations
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

		// Generates feedback for the user's guess compared to the secret number,
		// formatted as "Bulls,Cows".
		public override string GenerateFeedback(string secretNumber, string guess)
		{
			int bulls = CountBulls(secretNumber, guess);
			int cows = CountCows(secretNumber, guess);
			return $"{new string('B', bulls)},{new string('C', cows)}";
		}

		// Counts the number of bulls (correct digits in the correct positions).
		public override int CountBulls(string secretNumber, string guess)
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

		// Counts the number of cows (correct digits in incorrect positions)
		// in the guess compared to the secret number.
		public override int CountCows(string secretNumber, string guess)
		{
			int cows = 0;
			Dictionary<char, int> secretFrequency = [];

			// Populate frequency of non-bull digits from the secret number
			for (int i = 0; i < secretNumber.Length; i++)
			{
				if (secretNumber[i] != guess[i])
				{
					if (!secretFrequency.ContainsKey(secretNumber[i]))
						secretFrequency[secretNumber[i]] = 0;
					secretFrequency[secretNumber[i]]++;
				}
			}

			// Count cows (digits correct but in incorrect positions)
			for (int i = 0; i < guess.Length; i++)
			{
				if (secretNumber[i] != guess[i] &&
					secretFrequency.TryGetValue(guess[i], out int count) &&
					count > 0)
				{
					cows++;
					secretFrequency[guess[i]]--;
				}
			}

			return cows;
		}

		// Returns the type of game (Bulls and Cows).
		public override GameTypes GetGameType()
		{
			return GameTypes.BullsAndCows;
		}
	}
}