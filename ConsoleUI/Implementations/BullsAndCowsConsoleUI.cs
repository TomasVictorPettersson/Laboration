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
	"Welcome, {0}, to Bulls and Cows!\n\n" +
	"The objective of the game is to guess a 4-digit number.\n" +
	"Each digit in the 4-digit number will only appear once.\n" +
	"You can only use digits from 0 to 9.\n\n" +
	"For each guess, you will receive feedback in the form 'BBBB,CCCC', where:\n" +
	"- 'BBBB' represents the number of bulls (correct digits in correct positions).\n" +
	"- 'CCCC' represents the number of cows (correct digits in wrong positions).\n\n" +
	"If the feedback is 'No matches found',\n" + "it means none of the digits in your guess was found in the 4-digit number.\n";

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
				Console.Write("Enter your username: ");
				userName = Console.ReadLine()!;
				Console.WriteLine(_validation.ValidateUserName(userName));
			}
			while (!_validation.IsValidUserName(userName));
			Console.Clear();
			return userName;
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
				Console.WriteLine($"For practice mode, the 4-digit number is: {secretNumber}\n");
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
			Console.WriteLine($"Feedback: {(guessFeedback == "," ? "No matches found" : guessFeedback)}\n");
		}

		// Displays a message indicating the correct number and number of guesses taken.
		public void DisplayCorrectMessage(string secretNumber, int numberOfGuesses)
		{
			Console.WriteLine($"Correct! The 4-digit number was: {secretNumber}\nIt took you {numberOfGuesses} {(numberOfGuesses == 1 ? "guess" : "guesses")}.\n");
		}

		// Displays a custom message and waits for a key press before continuing.
		public void WaitForUserToContinue(string message)
		{
			Console.WriteLine(message);
			Console.ReadKey(intercept: true);
			Console.Clear();
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
				Console.WriteLine(CreateSeparatorLine(totalWidth));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error rendering high score list: {ex.Message}");
				throw;
			}
		}

		// Creates a separator line of the specified width using dashes.
		public string CreateSeparatorLine(int totalWidth)
		{
			return new string('-', totalWidth);
		}

		// Calculates the maximum username length and total display width for formatting.
		// Treats usernames shorter than six characters as if they were six characters long.
		public (int maxUserNameLength, int totalWidth) CalculateDisplayDimensions(List<IPlayerData> results)
		{
			int maxUserNameLength = Math.Max(results.Max(p => p.UserName.Length), 6);
			int totalWidth = RankColumnWidth + maxUserNameLength + GamesPlayedColumnWidth + AverageGuessesColumnWidth + Padding;
			return (maxUserNameLength, totalWidth);
		}

		// Displays the header for the high score list with proper formatting.
		public void DisplayHighScoreListHeader(int maxUserNameLength, int totalWidth)
		{
			const string header = "=== HIGH SCORE LIST ===";
			int leftPadding = (totalWidth - header.Length) / 2;

			string headerRowFormat = $"{"Rank",-RankColumnWidth} {"Player".PadRight(maxUserNameLength)} {"   Games",-GamesPlayedColumnWidth} {"   Avg. Guesses",-AverageGuessesColumnWidth}";

			string headerFormat = $"{new string(' ', leftPadding)}{header}\n" +
								  $"{CreateSeparatorLine(totalWidth)}\n" +
								  $"{headerRowFormat}\n" +
								  $"{CreateSeparatorLine(totalWidth)}";

			Console.WriteLine($"{headerFormat}");
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
			Console.WriteLine($" {player.UserName.PadRight(maxUserNameLength)} {player.TotalGamesPlayed,GamesPlayedColumnWidth} {player.CalculateAverageGuesses(),AverageGuessesColumnWidth:F2}");
		}

		// Asks the user if they want to continue playing or exit.
		public bool AskToContinue()
		{
			while (true)
			{
				Console.Write("Do you want to play again? (y/n): ");
				string answer = Console.ReadLine()!.ToLower();
				switch (answer)
				{
					case YesInput:
						Console.Clear();
						return true;

					case NoInput:
						return false;

					default:
						Console.WriteLine("Invalid input. Please enter y for yes or n for no.\n");
						break;
				}
			}
		}

		// Displays a personalized goodbye message to the user and prompts them to close the window.
		public void DisplayGoodbyeMessage(string userName)
		{
			Console.WriteLine($"Thank you, {userName}, for playing Bulls and Cows!");
			WaitForUserToContinue("\nPress any key to close this window...");
		}
	}
}