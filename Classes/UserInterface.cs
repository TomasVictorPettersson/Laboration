using Laboration.Interfaces;

namespace Laboration.Classes
{
	public class UserInterface : IUserInterface
	{
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

		public void DisplayCorrectMessage(string secretNumber, int numberOfGuesses)
		{
			Console.WriteLine($"\nCorrect! The secret number was: {secretNumber}\nIt took you {numberOfGuesses} guesses");
		}

		public bool AskToContinue()
		{
			while (true)
			{
				Console.Write("\nContinue? (y/n): ");
				string answer = Console.ReadLine()!;

				if (string.IsNullOrEmpty(answer))
				{
					Console.WriteLine("Empty values are not allowed. Please enter y for yes or n for no.");
				}
				else if (string.Equals(answer, "y", StringComparison.OrdinalIgnoreCase))
				{
					Console.Clear();
					return true;
				}
				else if (string.Equals(answer, "n", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				else
				{
					Console.WriteLine("Invalid input. Please enter y for yes or n for no.");
				}
			}
		}
	}
}