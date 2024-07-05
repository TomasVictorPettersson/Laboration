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
			_highScoreManager.ShowHighScoreList(userName);
			_userInterface.DisplayCorrectMessage(secretNumber, numberOfGuesses);
		}

		public void SaveResult(string userName, int numberOfGuesses)
		{
			using StreamWriter output = new("result.txt", append: true);
			output.WriteLine($"{userName}#&#{numberOfGuesses}");
		}

		public string MakeSecretNumber()
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

		public string GenerateBullsAndCowsFeedback(string secretNumber, string guess)
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