﻿using Laboration.UI.Interfaces;

namespace Laboration.UI.Classes
{
	// Implements IUserInterface to interact with the user through the console.
	public class UserInterface : IUserInterface
	{
		// Prompts the user to enter their username, ensuring it is not empty and within the specified length.
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
				else if (userName.Length < 2 || userName.Length > 20)
				{
					Console.WriteLine("Username must be between 2 and 20 characters long.");
				}
			}
			while (string.IsNullOrEmpty(userName) || userName.Length < 2 || userName.Length > 20);
			return userName;
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

		// Prompts the user for input until a non-empty string is entered.
		public string GetInputFromUser(string prompt)
		{
			string input = string.Empty;
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
			for (int retries = 0; retries < maxRetries; retries++)
			{
				string guess = GetInputFromUser("Enter your guess: ");
				if (IsInputValid(guess))
				{
					return guess;
				}
				Console.WriteLine("Invalid input. Please enter a 4-digit number.\n");
			}
			return string.Empty;
		}

		// Displays a message indicating the correct secret number and the number of guesses taken.
		public void DisplayCorrectMessage(string secretNumber, int numberOfGuesses)
		{
			Console.WriteLine($"\nCorrect! The secret number was: {secretNumber}\nIt took you {numberOfGuesses} guesses");
		}

		// Prompts the user to continue playing or exit the game.
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