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

		// Displays a welcome message to the player.
		public void DisplayWelcomeMessage(string userName)
		{
			Console.WriteLine($"Welcome {userName} to Bulls and Cows!");
			Console.WriteLine("The objective is to guess a 4-digit number.");
			Console.WriteLine("Feedback: 'BBBB' for bulls (correct in position), 'CCCC' for cows (correct in wrong position).\n");
		}

		// Initializes the game by clearing console and displaying welcome message.
		public void InitializeGame(string userName)
		{
			try
			{
				Console.Clear();
				DisplayWelcomeMessage(userName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error initializing game: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}

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

				while (!IsCorrectGuess(guess, secretNumber))
				{
					guess = ProcessGuess(secretNumber, ref numberOfGuesses);
				}

				EndGame(secretNumber, userName, numberOfGuesses);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in game loop: {ex.Message}");
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
				string guess = GetValidGuessFromUser(_config.MaxRetries);

				if (string.IsNullOrEmpty(guess))
				{
					return string.Empty;
				}

				numberOfGuesses++;
				string guessFeedback = GenerateBullsAndCowsFeedback(secretNumber, guess);
				Console.WriteLine($"{guessFeedback}\n");

				return guess;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error processing guess: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}

		// Prompts the user to enter a 4-digit number guess, validates it, and returns the guess.
		public static string GetValidGuessFromUser(int maxRetries)
		{
			try
			{
				string guess = string.Empty;
				int retries = 0;

				while (retries < maxRetries)
				{
					Console.Write("Enter your guess: ");
					guess = Console.ReadLine()!.Trim();

					if (string.IsNullOrEmpty(guess) || guess.Length != 4 || !int.TryParse(guess, out _))
					{
						Console.WriteLine("Invalid input. Please enter a 4-digit number.\n");
						retries++;
					}
					else
					{
						break;
					}
				}

				if (retries >= maxRetries)
				{
					return string.Empty;
				}

				return guess;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error getting valid guess from user: {ex.Message}");
				throw; // Rethrow the exception to propagate it upwards
			}
		}

		// Generates a random 4-digit secret number.
		public string MakeSecretNumber()
		{
			try
			{
				Random randomGenerator = new();
				HashSet<int> digits = [];

				while (digits.Count < 4)
				{
					int randomDigit = randomGenerator.Next(10);
					digits.Add(randomDigit);
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

		// Generates bulls and cows feedback based on the player's guess and the secret number.
		public static string GenerateBullsAndCowsFeedback(string secretNumber, string guess)
		{
			try
			{
				int cows = 0, bulls = 0;

				for (int i = 0; i < 4; i++)
				{
					if (secretNumber[i] == guess[i])
					{
						bulls++;
					}
					else if (secretNumber.Contains(guess[i]))
					{
						cows++;
					}
				}
				return $"{new string('B', bulls)},{new string('C', cows)}";
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error generating feedback: {ex.Message}");
				throw; // Propagate exception for higher level handling.
			}
		}
	}
}