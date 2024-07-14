using Laboration.UI.Interfaces;

namespace Laboration.UI.Classes
{
	// Implements IUserInterface to interact with the user through the console.
	public class UserInterface : IUserInterface
	{
		// Prompts the user to enter their username, ensuring it is not empty.
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
			Console.WriteLine("The objective of the game is to guess a 4-digit number.");
			Console.WriteLine("Each digit in the 4-digit number will only appear once.");
			Console.WriteLine("For each guess, you will receive feedback in the form of 'BBBB,CCCC',");
			Console.WriteLine("where 'BBBB' represents the number of bulls (correct digits in the correct positions),");
			Console.WriteLine("and 'CCCC' represents the number of cows (correct digits in the wrong positions).");
			Console.WriteLine("If you receive a response of only ',' it means none of the digits in your guess are present in the 4-digit number.\n");
		}

		// Prompts the user for input until a non-empty string is entered.
		public string GetInputFromUser(string prompt)
		{
			string input = string.Empty;

			// Continuously prompt the user until a non-empty input is received
			while (string.IsNullOrEmpty(input))
			{
				Console.Write(prompt);
				input = Console.ReadLine()!.Trim();
			}

			return input;
		}

		// Validates if the input is a non-empty string, exactly 4 characters long, and numeric.
		public bool IsInputValid(string input)
		{
			return !string.IsNullOrEmpty(input) && input.Length == 4 && int.TryParse(input, out _);
		}

		// Prompts the user to enter a valid 4-digit number, allowing a specified number of retries.
		public string GetValidGuessFromUser(int maxRetries)
		{
			// Loop until a valid guess is received or the maximum number of retries is reached
			for (int retries = 0; retries < maxRetries; retries++)
			{
				string guess = GetInputFromUser("Enter your guess: ");

				// Check if the guess is valid
				if (IsInputValid(guess))
				{
					return guess;
				}

				// Inform the user about invalid input and increment the retry counter
				Console.WriteLine("Invalid input. Please enter a 4-digit number.\n");
			}

			// Return an empty string if the maximum number of retries is reached
			return string.Empty;
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