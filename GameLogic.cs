using System.Text;

namespace MooGame
{
	public static class GameLogic
	{
		public static void PlayGame(string userName)
		{
			Console.Clear();
			string secretNumber = MakeSecretNumber();
			Console.WriteLine("New game:\n");

			////comment out or remove next line to play real games!
			Console.WriteLine($"For practice, number is: {secretNumber}\n");
			string guess = Console.ReadLine()!;

			int numberOfGuesses = 1;
			string guessFeedback = GenerateBullsAndCowsFeedback(secretNumber, guess);
			Console.WriteLine($"{guessFeedback}\n");
			while (!string.Equals(guessFeedback, "BBBB,", StringComparison.OrdinalIgnoreCase))
			{
				numberOfGuesses++;
				guess = Console.ReadLine()!;
				Console.WriteLine($"{guess}\n");
				guessFeedback = GenerateBullsAndCowsFeedback(secretNumber, guess);
				Console.WriteLine($"{guessFeedback}\n");
			}
			SaveResult(userName, numberOfGuesses);
			HighScoreManager.ShowHighScoreList(userName);
			UserInterface.DisplayCorrectMessage(secretNumber, numberOfGuesses);
		}

		private static void SaveResult(string userName, int numberOfGuesses)
		{
			using StreamWriter output = new("result.txt", append: true);
			output.WriteLine($"{userName}#&#{numberOfGuesses}");
		}

		private static string MakeSecretNumber()
		{
			Random randomGenerator = new();
			StringBuilder secretNumber = new();

			for (int i = 0; i < 4; i++)
			{
				int random = randomGenerator.Next(10);
				string randomDigit = $"{random}";

				while (secretNumber.ToString().Contains(randomDigit))
				{
					random = randomGenerator.Next(10);
					randomDigit = $"{random}";
				}
				secretNumber.Append(randomDigit);
			}
			return secretNumber.ToString();
		}

		private static string GenerateBullsAndCowsFeedback(string secretNumber, string guess)
		{
			int cows = 0, bulls = 0;
			guess += "    ";
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					if (secretNumber[i] == guess[j])
					{
						if (i == j)
						{
							bulls++;
						}
						else
						{
							cows++;
						}
					}
				}
			}
			return $"{"BBBB".AsSpan(0, bulls)},{"CCCC".AsSpan(0, cows)}";
		}
	}
}