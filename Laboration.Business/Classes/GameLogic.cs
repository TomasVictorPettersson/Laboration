using Laboration.Business.Interfaces;
using Laboration.Common.Interfaces;
using Laboration.UI.Interfaces;
using System.Text;

namespace Laboration.Business.Classes
{
	public class GameLogic : IGameLogic
	{
		private readonly IHighScoreManager _highScoreManager;
		private readonly IUserInterface _userInterface;

		public GameLogic(IHighScoreManager highScoreManager, IUserInterface userInterface)
		{
			_highScoreManager = highScoreManager;
			_userInterface = userInterface;
		}

		public void DisplayWelcomeMessage(string userName)
		{
			Console.WriteLine($"Welcome {userName} to Bulls and Cows!");
			Console.WriteLine("The objective of the game is to guess a 4-digit number.");
			Console.WriteLine("For each guess, you will receive feedback in the form of 'BBBB,CCCC',");
			Console.WriteLine("where 'BBBB' represents the number of bulls (correct digits in the correct positions),");
			Console.WriteLine("and 'CCCC' represents the number of cows (correct digits in the wrong positions).\n");
		}

		public void InitializeGame(string userName)
		{
			Console.Clear();
			DisplayWelcomeMessage(userName);
		}

		public void PlayGame(string userName)
		{
			InitializeGame(userName);
			string secretNumber = MakeSecretNumber();
			Console.WriteLine("New game:\n");
			DisplaySecretNumberForPractice(secretNumber);
			PlayGameLoop(secretNumber, userName);
		}

		public void DisplaySecretNumberForPractice(string secretNumber)
		{
			// Comment out or remove the next line to play real games!
			Console.WriteLine($"For practice, number is: {secretNumber}\n");
		}

		public void PlayGameLoop(string secretNumber, string userName)
		{
			int numberOfGuesses = 0;
			string guess = string.Empty;

			while (!IsCorrectGuess(guess, secretNumber))
			{
				guess = ProcessGuess(secretNumber, ref numberOfGuesses);
			}

			EndGame(secretNumber, userName, numberOfGuesses);
		}

		public bool IsCorrectGuess(string guess, string secretNumber)
		{
			return string.Equals(guess, secretNumber, StringComparison.OrdinalIgnoreCase);
		}

		public void EndGame(string secretNumber, string userName, int numberOfGuesses)
		{
			_highScoreManager.SaveResult(userName, numberOfGuesses);
			_highScoreManager.ShowHighScoreList(userName);
			_userInterface.DisplayCorrectMessage(secretNumber, numberOfGuesses);
		}

		public string ProcessGuess(string secretNumber, ref int numberOfGuesses)
		{
			string guess = GetValidGuessFromUser();

			if (string.IsNullOrEmpty(guess))
			{
				return string.Empty;
			}

			numberOfGuesses++;
			string guessFeedback = GenerateBullsAndCowsFeedback(secretNumber, guess);
			Console.WriteLine($"{guessFeedback}\n");

			return guess;
		}

		public string GetValidGuessFromUser()
		{
			string guess;

			while (true)
			{
				Console.Write("Enter your guess: ");
				guess = Console.ReadLine()?.Trim();

				if (string.IsNullOrEmpty(guess) || guess.Length != 4 || !int.TryParse(guess, out _))
				{
					Console.WriteLine("Invalid input. Please enter a 4-digit number.\n");
				}
				else
				{
					break;
				}
			}

			return guess;
		}

		public string MakeSecretNumber()
		{
			Random randomGenerator = new();
			HashSet<int> digits = new();

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