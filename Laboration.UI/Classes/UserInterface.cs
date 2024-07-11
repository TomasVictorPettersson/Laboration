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

		// Displays a message indicating the correct secret number and the number of guesses taken.
		// Parameters:
		//   secretNumber: The correct secret number.
		//   numberOfGuesses: The number of guesses taken to guess the secret number.
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
				string answer = Console.ReadLine()!;

				switch (answer.ToLower())
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