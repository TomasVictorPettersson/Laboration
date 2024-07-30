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
		private readonly IHighScoreManager _highScoreManager = highScoreManager ?? throw new ArgumentNullException(nameof(highScoreManager));
		private readonly IConsoleUI _consoleUI = consoleUI ?? throw new ArgumentNullException(nameof(consoleUI));
		private readonly IValidation _validation = validation ?? throw new ArgumentNullException(nameof(validation));

		// Starts the game by generating a secret number and initiating the game loop.
		public void PlayGame(string userName)
		{
			try
			{
				InitializeGame(userName);
				string secretNumber = MakeSecretNumber();
				_consoleUI.DisplaySecretNumberForPractice(secretNumber);
				PlayGameLoop(secretNumber, userName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error playing game: {ex.Message}");
				throw;
			}
		}

		// Displays a welcome message to the user.
		public void InitializeGame(string userName)
		{
			try
			{
				_consoleUI.DisplayWelcomeMessage(userName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error initializing game: {ex.Message}");
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

		// Main game loop.
		public void PlayGameLoop(string secretNumber, string userName)
		{
			try
			{
				int numberOfGuesses = 0;
				string guess = string.Empty;

				while (!_validation.IsCorrectGuess(guess, secretNumber))
				{
					guess = ProcessGuess(secretNumber, ref numberOfGuesses);
					if (_validation.IsCorrectGuess(guess, secretNumber))
					{
						break;
					}
				}

				EndGame(secretNumber, userName, numberOfGuesses);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in game loop: {ex.Message}");
				throw;
			}
		}

		// Processes the player's guess, validates it, and provides feedback.
		public string ProcessGuess(string secretNumber, ref int numberOfGuesses)
		{
			try
			{
				string guess = _consoleUI.GetValidGuessFromUser();

				if (string.IsNullOrEmpty(guess) || !_validation.IsInputValid(guess))
				{
					Console.WriteLine("Guess is empty or invalid.");
					return string.Empty;
				}

				string guessFeedback = GenerateBullsAndCowsFeedback(secretNumber, guess);
				Console.WriteLine($"{guessFeedback}\n");
				numberOfGuesses++;

				return _validation.IsCorrectGuess(guess, secretNumber) ? guess : guessFeedback;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error processing guess: {ex.Message}");
				throw;
			}
		}

		// Ends the game, saves the result, and displays high scores.
		public void EndGame(string secretNumber, string userName, int numberOfGuesses)
		{
			try
			{
				_highScoreManager.SaveResult(userName, numberOfGuesses);
				_consoleUI.DisplayHighScoreList(userName);
				_consoleUI.DisplayCorrectMessage(secretNumber, numberOfGuesses);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error ending game: {ex.Message}");
				throw;
			}
		}

		// Generates feedback for the player's guess in terms of bulls and cows.
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

		// Counts the number of bulls (correct digits in correct positions).
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

		// Counts the number of cows (correct digits in wrong positions).
		public static int CountCows(string secretNumber, string guess)
		{
			try
			{
				int cows = 0;
				Dictionary<char, int> digitFrequency = [];

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
			catch (Exception ex)
			{
				Console.WriteLine($"Error counting cows: {ex.Message}");
				throw;
			}
		}
	}
}