using Laboration.Business.Interfaces;
using Laboration.Common.Interfaces;
using Laboration.UI.Interfaces;
using System.Text;

namespace Laboration.Business.Classes
{
	public class GameLogic(IHighScoreManager highScoreManager, IUserInterface userInterface) : IGameLogic
	{
		private readonly IHighScoreManager _highScoreManager = highScoreManager;
		private readonly IUserInterface _userInterface = userInterface;

		public void PlayGame(string userName)
		{
			Console.Clear();
			Console.WriteLine($"Welcome {userName} to Bulls and Cows!");
			Console.WriteLine("The objective of the game is to guess a 4-digit number.");
			Console.WriteLine("For each guess, you will receive feedback in the form of 'BBBB,CCCC',");
			Console.WriteLine("where 'BBBB' represents the number of bulls (correct digits in the correct positions),");
			Console.WriteLine("and 'CCCC' represents the number of cows (correct digits in the wrong positions).\n");
			string secretNumber = MakeSecretNumber();
			Console.WriteLine("New game:\n");

			// Comment out or remove the next line to play real games!
			Console.WriteLine($"For practice, number is: {secretNumber}\n");
			int numberOfGuesses = 0;
			string guess = string.Empty;
			while (!string.Equals(guess, secretNumber, StringComparison.OrdinalIgnoreCase))
			{
				Console.Write("Enter your guess: ");
				guess = Console.ReadLine()!;

				// Validate user input
				if (guess.Length != 4 || !int.TryParse(guess, out _))
				{
					Console.WriteLine("Invalid input. Please enter a 4-digit number.\n");
					continue;
				}

				numberOfGuesses++;
				string guessFeedback = GenerateBullsAndCowsFeedback(secretNumber, guess);
				Console.WriteLine($"{guessFeedback}\n");
			}

			_highScoreManager.SaveResult(userName, numberOfGuesses);
			_highScoreManager.ShowHighScoreList(userName);
			_userInterface.DisplayCorrectMessage(secretNumber, numberOfGuesses);
		}

		public string MakeSecretNumber()
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

		public string GenerateBullsAndCowsFeedback(string secretNumber, string guess)
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
	}
}