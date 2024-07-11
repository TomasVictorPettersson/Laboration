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
		// Processes the player's guess, validates it, and provides feedback.
		public string ProcessGuess(string secretNumber, ref int numberOfGuesses)
		{
			try
			{
				string guess = _userInterface.GetValidGuessFromUser(_config.MaxRetries);

				if (string.IsNullOrEmpty(guess))
				{
					// Return specific feedback indicating invalid input
					return "Invalid input. Please enter a 4-digit number.";
				}

				numberOfGuesses++;
				string guessFeedback = GenerateBullsAndCowsFeedback(secretNumber, guess);
				Console.WriteLine($"{guessFeedback}\n");

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