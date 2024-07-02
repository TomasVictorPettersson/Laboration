using System.Text;

namespace MooGame
{
	internal static class Program
	{
		private static string GetUserName()
		{
			string userName;
			do
			{
				Console.WriteLine("Enter your user name:\n");
				userName = Console.ReadLine()!;

				if (string.IsNullOrEmpty(userName))
				{
					Console.WriteLine("Empty values are not allowed. Please enter a valid username.");
				}
			}
			while (string.IsNullOrEmpty(userName));

			return userName;
		}

		public static void Main()
		{
			string userName = GetUserName();
			bool playOn = true;
			while (playOn)
			{
				string goal = MakeGoal();
				Console.WriteLine("New game:\n");
				//comment out or remove next line to play real games!
				Console.WriteLine($"For practice, number is: {goal}\n");
				string guess = Console.ReadLine()!;

				int numberOfGuesses = 1;
				string bbcc = CheckBC(goal, guess);
				Console.WriteLine($"{bbcc}\n");
				while (!string.Equals(bbcc, "BBBB,", StringComparison.OrdinalIgnoreCase))
				{
					numberOfGuesses++;
					guess = Console.ReadLine()!;
					Console.WriteLine($"{guess}\n");
					bbcc = CheckBC(goal, guess);
					Console.WriteLine($"{bbcc}\n");
				}
				using (StreamWriter output = new("result.txt", append: true))
				{
					output.WriteLine($"{userName}#&#{numberOfGuesses}");
				}
				ShowTopList();
				Console.WriteLine($"Correct, it took {numberOfGuesses} guesses\nContinue?");
				string answer = Console.ReadLine()!;
				if (!string.IsNullOrEmpty(answer) && string.Equals(answer, "n", StringComparison.OrdinalIgnoreCase))
				{
					playOn = false;
				}
			}
		}

		private static string MakeGoal()
		{
			Random randomGenerator = new();
			StringBuilder goal = new();

			for (int i = 0; i < 4; i++)
			{
				int random = randomGenerator.Next(10);
				string randomDigit = $"{random}";

				while (goal.ToString().Contains(randomDigit))
				{
					random = randomGenerator.Next(10);
					randomDigit = $"{random}";
				}
				goal.Append(randomDigit);
			}
			return goal.ToString();
		}

		private static string CheckBC(string goal, string guess)
		{
			int cows = 0, bulls = 0;
			guess += "    ";     // if player entered less than 4 chars
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					if (goal[i] == guess[j])
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
			using StreamReader input = new("result.txt");
			List<PlayerData> results = [];
			string line;
			while ((line = input.ReadLine()!) != null)
			{
				string[] nameAndScore = line.Split(separator, StringSplitOptions.None);
				string name = nameAndScore[0];
				int guesses = Convert.ToInt32(nameAndScore[1]);
				PlayerData pd = new(name, guesses);
				int pos = results.IndexOf(pd);
				if (pos < 0)
				{
					results.Add(pd);
				}
				else
				{
					results[pos].Update(guesses);
				}
			}
			results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
			Console.WriteLine("Player   games average");
			foreach (PlayerData p in results)
			{
				Console.WriteLine($"{p.Name,-9}{p.NGames,5:D}{p.Average(),9:F2}");
			}
		}
	}
}