using System.Text;

namespace MooGame
{
	public static class GameLogic
	{
		public static string GetUserName()
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
			ShowTopList();
			Console.WriteLine($"\nCorrect, it took {numberOfGuesses} guesses");
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
			guess += "    ";     // if player entered less than 4 chars
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

		private static readonly string[] separator = ["#&#"];

		private static void ShowTopList()
		{
			Console.Clear();
			Console.WriteLine("=== High Score List ===");
			Console.WriteLine("Player       Games   Average Guesses");
			Console.WriteLine("------------------------------------");
			using StreamReader input = new("result.txt");
			List<PlayerData> results = [];
			string line;
			while ((line = input.ReadLine()!) != null)
			{
				string[] userNameAndUserScore = line.Split(separator, StringSplitOptions.None);
				string userName = userNameAndUserScore[0];
				int guesses = Convert.ToInt32(userNameAndUserScore[1]);
				PlayerData pd = new(userName, guesses);
				int pos = results.FindIndex(p => p.Equals(pd));
				if (pos < 0)
				{
					results.Add(pd);
				}
				else
				{
					results[pos].AddGuess(guesses);
				}
			}
			results.Sort((p1, p2) => p1.CalculateAverageGuesses().CompareTo(p2.CalculateAverageGuesses()));

			foreach (PlayerData p in results)
			{
				Console.WriteLine($"{p.UserName,-12}{p.TotalGamesPlayed,7}{p.CalculateAverageGuesses(),14:F2}");
			}
		}

		public static bool AskToContinue()
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