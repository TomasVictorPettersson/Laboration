using Laboration.Business.Interfaces;
using Laboration.Common.Interfaces;
using Laboration.UI.Interfaces;
using System.Text;

namespace Laboration.Business.Classes
{
	// Handles the game logic for Bulls and Cows game, including initialization, gameplay, and feedback generation.
	public class GameLogic(IHighScoreManager highScoreManager, IUserInterface userInterface, GameConfig config) : IGameLogic
	{
		private readonly IHighScoreManager _highScoreManager = highScoreManager ?? throw new ArgumentNullException(nameof(highScoreManager));
		private readonly IUserInterface _userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
		private readonly GameConfig _config = config ?? throw new ArgumentNullException(nameof(config));

		// Starts the game by generating a secret number and initiating the game loop.
		public void PlayGame(string userName)
		{
			try
			{
				InitializeGame(userName);
				string secretNumber = MakeSecretNumber();
				Console.WriteLine("New game:\n");
				DisplaySecretNumberForPractice(secretNumber);
				PlayGameLoop(secretNumber, userName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error playing game: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}

		// Initializes the game by displaying a welcome message.
		public void InitializeGame(string userName)
		{
			try
			{
				_userInterface.DisplayWelcomeMessage(userName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error initializing game: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}

		// Displays the secret number (for practice mode only).
		public void DisplaySecretNumberForPractice(string secretNumber)
		{
			try
			{
				Console.WriteLine($"For practice, number is: {secretNumber}\n");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error displaying secret number: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}

		// Executes the game loop until the player guesses the secret number correctly.
		public void PlayGameLoop(string secretNumber, string userName)
		{
			try
			{
				int numberOfGuesses = 0;
				string guess = string.Empty;

				// Loop until the correct guess is made
				while (!IsCorrectGuess(guess, secretNumber))
				{
					guess = ProcessGuess(secretNumber, ref numberOfGuesses);

					// Check if the guess is correct to exit the loop
					if (IsCorrectGuess(guess, secretNumber))
					{
						break;
					}
				}

				EndGame(secretNumber, userName, numberOfGuesses);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in game loop: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}

		// Ends the game, saves the result, shows high scores, and displays correct message.
		public void EndGame(string secretNumber, string userName, int numberOfGuesses)
		{
			try
			{
				_highScoreManager.SaveResult(userName, numberOfGuesses);
				_highScoreManager.ShowHighScoreList(userName);
				_userInterface.DisplayCorrectMessage(secretNumber, numberOfGuesses);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error ending game: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}

		// Processes the player's guess, validates it, and provides feedback.
		public string ProcessGuess(string secretNumber, ref int numberOfGuesses)
		{
			try
			{
				string guess = _userInterface.GetValidGuessFromUser(_config.MaxRetries);

				// Check if guess is empty (handled in GetValidGuessFromUser)
				if (string.IsNullOrEmpty(guess))
				{
					return string.Empty;
				}

				// Generate and display feedback
				string guessFeedback = GenerateBullsAndCowsFeedback(secretNumber, guess);
				Console.WriteLine($"{guessFeedback}\n");
				numberOfGuesses++;

				// If guess is correct, return the guess itself
				if (IsCorrectGuess(guess, secretNumber))
				{
					return guess;
				}

				return guessFeedback;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error processing guess: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}

		// Generates a random 4-digit secret number.
		public string MakeSecretNumber()
		{
			try
			{
				Random randomGenerator = new();
				List<int> digits = [];

				while (digits.Count < 4)
				{
					int randomDigit = randomGenerator.Next(10);
					if (!digits.Contains(randomDigit))
					{
						digits.Add(randomDigit);
					}
				}

				StringBuilder secretNumber = new();
				foreach (int digit in digits)
				{
					secretNumber.Append(digit);
				}

				return secretNumber.ToString();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error making secret number: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}

		// Checks if the player's guess matches the secret number.
		public static bool IsCorrectGuess(string guess, string secretNumber)
		{
			try
			{
				return string.Equals(guess, secretNumber, StringComparison.OrdinalIgnoreCase);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error checking guess correctness: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}

		// Generates bulls and cows feedback based on the player's guess and the secret number.
		public static string GenerateBullsAndCowsFeedback(string secretNumber, string guess)
		{
			try
			{
				// Count bulls and cows using separate methods
				int bulls = CountBulls(secretNumber, guess);
				int cows = CountCows(secretNumber, guess);

				// Format the feedback string based on bulls and cows counts
				return $"{new string('B', bulls)},{new string('C', cows)}";
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error generating feedback: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}

		// Counts the number of bulls (correct digits in correct positions) between secretNumber and guess.
		public static int CountBulls(string secretNumber, string guess)
		{
			try
			{
				int bulls = 0;

				// Count bulls by comparing characters at each position
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
				throw; // Propagate exception for higher level handling.
			}
		}

		// Counts the number of cows (correct digits in wrong positions) between secretNumber and guess.
		public static int CountCows(string secretNumber, string guess)
		{
			try
			{
				int cows = 0;

				// Count cows by checking if guess characters exist in secretNumber but are not in the same position
				for (int i = 0; i < 4; i++)
				{
					if (secretNumber[i] != guess[i] && secretNumber.Contains(guess[i]))
					{
						cows++;
					}
				}
				return cows;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error counting cows: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}
	}
}