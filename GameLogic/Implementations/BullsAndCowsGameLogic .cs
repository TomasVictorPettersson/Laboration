using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using System.Text;

namespace Laboration.GameLogic.Implementations
{
	// Manages the Bulls and Cows game logic, including setup, gameplay, and result handling.
	public class BullsAndCowsGameLogic(IHighScoreManager highScoreManager, IConsoleUI consoleUI, IValidation validation) : IGameLogic
	{
		private readonly IHighScoreManager _highScoreManager = highScoreManager;
		private readonly IConsoleUI _consoleUI = consoleUI;
		private readonly IValidation _validation = validation;

		// Starts the game by displaying a welcome message, generating a secret number,
		// and then running the main game loop.
		public void PlayGame(string userName)
		{
			try
			{
				_consoleUI.DisplayWelcomeMessage(userName);
				string secretNumber = MakeSecretNumber();
				// Comment out or remove next line to play the real game!
				_consoleUI.DisplaySecretNumberForPractice(secretNumber);
				PlayGameLoop(secretNumber, userName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error playing game: {ex.Message}");
				throw;
			}
		}

		// Generates a random 4-digit secret number.
		public string MakeSecretNumber()
		{
			Random randomGenerator = new();
			StringBuilder secretNumber = new();
			HashSet<int> usedDigits = [];

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

		// Main game loop that continues until the user guesses the secret number.
		public void PlayGameLoop(string secretNumber, string userName)
		{
			try
			{
				int numberOfGuesses = 0;
				bool isGuessCorrect = false;

				while (!isGuessCorrect)
				{
					isGuessCorrect = HandleUserGuess(secretNumber, ref numberOfGuesses);
				}

				EndGame(secretNumber, userName, numberOfGuesses);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in game loop: {ex.Message}");
				throw;
			}
		}

		// Retrieves the user's guess from the console UI and processes it.
		public bool HandleUserGuess(string secretNumber, ref int numberOfGuesses)
		{
			string guess = _consoleUI.GetValidGuessFromUser();

			return ProcessGuess(secretNumber, guess, ref numberOfGuesses);
		}

		// Processes the user's guess, generates feedback, and updates the number of guesses.
		public bool ProcessGuess(string secretNumber, string guess, ref int numberOfGuesses)
		{
			string guessFeedback = GenerateBullsAndCowsFeedback(secretNumber, guess);

			_consoleUI.DisplayGuessFeedback(guessFeedback);

			numberOfGuesses++;

			return _validation.IsCorrectGuess(guess, secretNumber);
		}

		// Ends the game by saving the player's result, displaying a message with the
		// correct number and number of guesses, and showing the updated high score list.
		public void EndGame(string secretNumber, string userName, int numberOfGuesses)
		{
			try
			{
				_highScoreManager.SaveResult(userName, numberOfGuesses);
				_consoleUI.DisplayCorrectMessage(secretNumber, numberOfGuesses);
				_consoleUI.DisplayHighScoreList(userName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error ending game: {ex.Message}");
				throw;
			}
		}

		// Generates feedback for the player's guess in the form of 'BBBB,CCCC',
		// where 'BBBB' is the count of bulls (correct digits in correct positions),
		// and 'CCCC' is the count of cows (correct digits in wrong positions).
		public static string GenerateBullsAndCowsFeedback(string secretNumber, string guess)
		{
			try
			{
				int bulls = CountBulls(secretNumber, guess);
				int cows = CountCows(secretNumber, guess);

				return $"{new string('B', bulls)},{new string('C', cows)}";
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error generating feedback: {ex.Message}");
				throw;
			}
		}

		// Counts the number of bulls in the guess compared to the secret number.
		// Bulls are defined as digits that are both correct and in the correct position.
		public static int CountBulls(string secretNumber, string guess)
		{
			try
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
			catch (Exception ex)
			{
				Console.WriteLine($"Error counting bulls: {ex.Message}");
				throw;
			}
		}

		// Counts the number of cows in the guess compared to the secret number.
		// Cows are defined as digits that are correct but in the wrong position.
		// The method also ensures not to count digits more than once.
		public static int CountCows(string secretNumber, string guess)
		{
			try
			{
				int cows = 0;
				Dictionary<char, int> digitFrequency = [];

				// Count frequency of each digit in the secret number.
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

				// Count cows by checking digits in the guess against the secret number.
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
			catch (Exception ex)
			{
				Console.WriteLine($"Error counting cows: {ex.Message}");
				throw;
			}
		}
	}
}