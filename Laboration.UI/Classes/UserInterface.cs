using Laboration.UI.Interfaces;

namespace Laboration.UI.Classes
{
	// Implements IUserInterface to interact with the user through the console.
	public class UserInterface : IUserInterface
	{
		// Prompts the user to enter their username, ensuring it is not empty.
		// Returns: The entered username.
		public string GetUserName()
		{
			string userName;
			do
			{
				Console.Write("Enter your user name: ");
				userName = Console.ReadLine()!;
				if (string.IsNullOrEmpty(userName))
				{
					Console.WriteLine("Empty values are not allowed. Please enter a valid username.");
				}
			}
			while (string.IsNullOrEmpty(userName));

			return userName;
		}

		// Displays a welcome message to the player.
		public void DisplayWelcomeMessage(string userName)
		{
			Console.Clear();
			Console.WriteLine($"Welcome {userName} to Bulls and Cows!");
			Console.WriteLine("The objective is to guess a 4-digit number.");
			Console.WriteLine("Feedback: 'BBBB' for bulls (correct in position), 'CCCC' for cows (correct in wrong position).\n");
		}

		// Prompts the user to enter a 4-digit number guess, validates it, and returns the guess.
		public string GetValidGuessFromUser(int maxRetries)
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

		// Displays a message indicating the correct secret number and the number of guesses taken.
		public void DisplayCorrectMessage(string secretNumber, int numberOfGuesses)
		{
			Console.WriteLine($"\nCorrect! The secret number was: {secretNumber}\nIt took you {numberOfGuesses} guesses");
		}

		// Prompts the user to continue playing or exit the game.
		// Returns: True if the user wants to continue, false otherwise.
		public bool AskToContinue()
		{
			while (true)
			{
				Console.Write("\nContinue? (y/n): ");
				string answer = Console.ReadLine()!.ToLower();

				switch (answer)
				{
					case "y":
						Console.Clear();
						return true;

					case "n":
						return false;

					default:
						Console.WriteLine("Invalid input. Please enter y for yes or n for no.");
						break;
				}
			}
		}
	}
}