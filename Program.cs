using System.Text;

namespace MooGame
{
	internal static class Program
	{
		public static void Main()
		{
			Console.WriteLine("Enter your user name:\n");
			string userName = Console.ReadLine()!;
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
				while (bbcc != "BBBB,")
				{
					numberOfGuesses++;
					guess = Console.ReadLine()!;
					Console.WriteLine($"{guess}\n");
					bbcc = CheckBC(goal, guess);
					Console.WriteLine($"{bbcc}\n");
				}
				StreamWriter output = new("result.txt", append: true);
				output.WriteLine($"{userName}#&#{numberOfGuesses}");
				output.Close();
				ShowTopList();
				Console.WriteLine($"Correct, it took {numberOfGuesses} guesses\nContinue?");
				string answer = Console.ReadLine()!;
				if (!string.IsNullOrEmpty(answer) && answer[..1] == "n")
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
				string randomDigit = "" + random;

				while (goal.ToString().Contains(randomDigit))
				{
					random = randomGenerator.Next(10);
					randomDigit = "" + random;
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

		private static void ShowTopList()
		{
			StreamReader input = new("result.txt");
			List<PlayerData> results = [];
			string line;
			while ((line = input.ReadLine()!) != null)
			{
				string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
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
				Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NGames, p.Average()));
			}
			input.Close();
		}
	}

	internal class PlayerData(string name, int guesses)
	{
		public string Name { get; } = name;
		public int NGames { get; private set; } = 1;
		private int totalGuess = guesses;

		public void Update(int guesses)
		{
			totalGuess += guesses;
			NGames++;
		}

		public double Average()
		{
			return (double)totalGuess / NGames;
		}

		public override bool Equals(Object p)
		{
			return Name.Equals(((PlayerData)p).Name);
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}
	}
}