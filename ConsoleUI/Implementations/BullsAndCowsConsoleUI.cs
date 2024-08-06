using Laboration.ConsoleUI.Interfaces;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.PlayerData.Interfaces;
using Laboration.Validation.Interfaces;

namespace Laboration.ConsoleUI.Implementations
{
	// Handles user interactions for the Bulls and Cows game in a console application.
	public class BullsAndCowsConsoleUI(IValidation validation, IHighScoreManager highScoreManager) : IConsoleUI
	{
		private const string WelcomeMessageFormat =
	"Welcome {0} to Bulls and Cows!\n\n" +
	"The objective of the game is to guess a 4-digit number.\n" +
	"Each digit in the 4-digit number will only appear once.\n" +
	"You can only use digits from 0 to 9.\n\n" +
	"For each guess, you will receive feedback in the form of 'BBBB,CCCC',\n" +
	"where 'BBBB' represents the number of bulls (correct digits in the correct positions),\n" +
	"and 'CCCC' represents the number of cows (correct digits in the wrong positions).\n" +
	"If you receive a response of only ',' it means none of the digits in your guess are present in the 4-digit number.\n\n";

		private const string YesInput = "y";
		private const string NoInput = "n";
		private const int RankColumnWidth = 6;
		private const int GamesPlayedColumnWidth = 8;
		private const int AverageGuessesColumnWidth = 15;
		private const int Padding = 3;

		private readonly IValidation _validation = validation;
		private readonly IHighScoreManager _highScoreManager = highScoreManager;

		// Prompts the user to enter their username, validating its length.
		public string GetUserName()
		{
			string userName;
			do
			{
				Console.Write("Enter your user name: ");
				userName = Console.ReadLine()!;
				Console.WriteLine(_validation.ValidateUserName(userName));
			}
			while (!_validation.IsValidUserName(userName));
			ClearConsole();
			return userName;
		}

		// Clears the console screen.
		public void ClearConsole()
		{
			Console.Clear();
		}

		// Displays a welcome message to the player.
		public void DisplayWelcomeMessage(string userName)
		{
			Console.WriteLine(string.Format(WelcomeMessageFormat, userName));
		}

		// Displays the secret number for practice mode.
		public void DisplaySecretNumberForPractice(string secretNumber)
		{
			try
			{
				Console.WriteLine($"For practice, number is: {secretNumber}\n");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error displaying secret number: {ex.Message}");
				throw;
			}
		}

		// Prompts the user to enter a valid 4-digit guess.
		public string GetValidGuessFromUser()
		{
			string guess;
			do
			{
				guess = GetInputFromUser();
				if (!_validation.IsInputValid(guess))
				{
					Console.WriteLine("Invalid input. Please enter a 4-digit number with unique digits.\n");
				}
			} while (!_validation.IsInputValid(guess));
			return guess;
		}

		// Gets input from the user.
		public string GetInputFromUser()
		{
			Console.Write("Enter your 4-digit guess: ");
			return Console.ReadLine()!.Trim();
		}

		// Displays feedback for the player's guess.
		public void DisplayGuessFeedback(string guessFeedback)
		{
			Console.WriteLine($"{guessFeedback}\n");
		}

		// Displays a message indicating the correct number and number of guesses taken.
		public void DisplayCorrectMessage(string secretNumber, int numberOfGuesses)
		{
			Console.WriteLine($"\nCorrect! The secret number was: {secretNumber}\nIt took you {numberOfGuesses} guesses.");
		}

		// Displays the high score list with formatted player data and highlights the current user.
		public void DisplayHighScoreList(string currentUserName)
		{
			try
			{
				var results = _highScoreManager.ReadHighScoreResultsFromFile();
				_highScoreManager.SortHighScoreList(results);
				RenderHighScoreList(results, currentUserName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error showing high score list: {ex.Message}");
				throw;
			}
		}

		// Displays the high score list with headers and formatted player data.
		public void RenderHighScoreList(List<IPlayerData> results, string currentUserName)
		{
			try
			{
				var (maxUserNameLength, totalWidth) = CalculateDisplayDimensions(results);
				DisplayHighScoreListHeader(maxUserNameLength, totalWidth);
				PrintHighScoreResults(results, currentUserName, maxUserNameLength);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error rendering high score list: {ex.Message}");
				throw;
			}
		}

		// Calculates the maximum username length and total display width for formatting.
		public (int maxUserNameLength, int totalWidth) CalculateDisplayDimensions(List<IPlayerData> results)
		{
			int maxUserNameLength = results.Max(p => p.UserName.Length);
			int totalWidth = RankColumnWidth + maxUserNameLength + GamesPlayedColumnWidth + AverageGuessesColumnWidth + Padding;

			return (maxUserNameLength, totalWidth);
		}

		// Displays the header for the high score list with proper formatting.
		public void DisplayHighScoreListHeader(int maxUserNameLength, int totalWidth)
		{
			const string header = "=== High Score List ===";
			int leftPadding = (totalWidth - header.Length) / 2;

			// Define the header format using constants
			// Define the header format using constants
			string headerFormat = $"\n{new string(' ', leftPadding)}{header}\n" +
								   $"{"Rank",-RankColumnWidth} {"Player".PadRight(maxUserNameLength)} {"Games",-GamesPlayedColumnWidth} {"Average Guesses",-AverageGuessesColumnWidth}\n" +
								   new string('-', totalWidth);
			// Output the formatted header
			Console.WriteLine(headerFormat);
		}

		// Displays the list of player data in a formatted manner, highlighting the current user.
		public void PrintHighScoreResults(List<IPlayerData> results, string currentUserName, int maxUserNameLength)
		{
			try
			{
				int rank = 1;
				foreach (IPlayerData p in results)
				{
					bool isCurrentUser = p.UserName.Equals(currentUserName, StringComparison.OrdinalIgnoreCase);
					SetConsoleColor(isCurrentUser);
					DisplayRank(rank);
					DisplayPlayerData(p, isCurrentUser, maxUserNameLength);
					Console.ResetColor();
					rank++;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error displaying high score results: {ex.Message}");
				throw;
			}
		}

		// Sets console color based on whether the player is the current user.
		public void SetConsoleColor(bool isCurrentUser)
		{
			Console.ForegroundColor = isCurrentUser ? ConsoleColor.Green : ConsoleColor.White;
		}

		// Displays the rank of the player.
		public void DisplayRank(int rank)
		{
			Console.Write($"{rank,-RankColumnWidth}");
		}

		// Displays detailed player data, with special formatting for the current user.
		public void DisplayPlayerData(IPlayerData player, bool isCurrentUser, int maxUserNameLength)
		{
			Console.WriteLine($"{player.UserName.PadRight(maxUserNameLength)} {player.TotalGamesPlayed,GamesPlayedColumnWidth} {player.CalculateAverageGuesses(),AverageGuessesColumnWidth:F2}");
		}

		// Asks the user if they want to continue playing or exit.
		public bool AskToContinue()
		{
			while (true)
			{
				Console.Write("\nContinue? (y/n): ");
				string answer = Console.ReadLine()!.ToLower();
				switch (answer)
				{
					case YesInput:
						ClearConsole();
						return true;

					case NoInput:
						return false;

					default:
						Console.WriteLine("Invalid input. Please enter y for yes or n for no.");
						break;
				}
			}
		}

		// Displays a farewell message to the user after the game ends.
		public void DisplayGoodbyeMessage(string userName)
		{
			Console.WriteLine($"Thank you, {userName}, for playing Bulls and Cows!");
		}
	}
}