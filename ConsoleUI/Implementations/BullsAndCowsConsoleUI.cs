using Laboration.ConsoleUI.Interfaces;

namespace Laboration.ConsoleUI.Implementations
{
	// Handles user interactions for the Bulls and Cows game in a console application.
	public class BullsAndCowsConsoleUI : IConsoleUI
	{
		// Prompts the user to enter a username, validating its length.
		public string GetUserName()
		{
			string userName;
			do
			{
				Console.Write("Enter your user name: ");
				userName = Console.ReadLine()!;
				ValidateUserName(userName);
			}
			while (!IsValidUserName(userName));
			return userName;
		}

		// Validates username and prints error messages if invalid.
		public void ValidateUserName(string userName)
		{
			if (string.IsNullOrEmpty(userName))
			{
				Console.WriteLine("Empty values are not allowed. Please enter a valid username.");
			}
			else if (userName.Length < 2 || userName.Length > 20)
			{
				Console.WriteLine("Username must be between 2 and 20 characters long.");
			}
		}

		// Checks if the username is valid.
		public bool IsValidUserName(string userName)
		{
			return !string.IsNullOrEmpty(userName) && userName.Length >= 2 && userName.Length <= 20;
		}

		// Displays a welcome message to the player.
		public void DisplayWelcomeMessage(string userName)
		{
			Console.Clear();
			Console.WriteLine($"Welcome {userName} to Bulls and Cows!");
			Console.WriteLine("\nThe objective of the game is to guess a 4-digit number.");
			Console.WriteLine("Each digit in the 4-digit number will only appear once.");
			Console.WriteLine("\nFor each guess, you will receive feedback in the form of 'BBBB,CCCC',");
			Console.WriteLine("where 'BBBB' represents the number of bulls (correct digits in the correct positions),");
			Console.WriteLine("and 'CCCC' represents the number of cows (correct digits in the wrong positions).");
			Console.WriteLine("If you receive a response of only ',' it means none of the digits in your guess are present in the 4-digit number.\n");
		}

		// Prompts the user to enter a valid 4-digit guess.
		public string GetValidGuessFromUser()
		{
			string guess;
			do
			{
				guess = GetInputFromUser("Enter your guess: ");
				if (!IsInputValid(guess))
				{
					Console.WriteLine("Invalid input. Please enter a 4-digit number with unique digits.\n");
				}
			} while (!IsInputValid(guess));
			return guess;
		}

		// Gets input from the user with a prompt.
		public string GetInputFromUser(string prompt)
		{
			string input;
			do
			{
				Console.Write(prompt);
				input = Console.ReadLine()!.Trim();
			} while (string.IsNullOrEmpty(input));
			return input;
		}

		// Validates if the input is a valid 4-digit number with unique digits.
		public bool IsInputValid(string input)
		{
			return !string.IsNullOrEmpty(input) && input.Length == 4 && int.TryParse(input, out _) && input.Distinct().Count() == 4;
		}

		// Displays a message indicating the correct number and number of guesses taken.
		public void DisplayCorrectMessage(string secretNumber, int numberOfGuesses)
		{
			Console.WriteLine($"\nCorrect! The secret number was: {secretNumber}\nIt took you {numberOfGuesses} guesses.");
		}

		// Prompts the user to continue playing or exit.
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

		// Displays a goodbye message to the user.
		public void DisplayGoodbyeMessage(string userName)
		{
			Console.WriteLine($"Thank you, {userName}, for playing Bulls and Cows!");
		}
	}
}