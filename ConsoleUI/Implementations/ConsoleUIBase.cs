using ConsoleUI.Interfaces;
using ConsoleUI.Utils;
using GameResources.Constants;
using GameResources.Enums;
using HighScoreManagement.Interfaces;
using PlayerData.Interfaces;
using Validation.Interfaces;

namespace ConsoleUI.Implementations
{
	// Base class for Console UI handling common operations for different games.
	// Implements IConsoleUI and contains shared logic for all console-based user interactions.
	// Uses game-specific implementations for welcome, goodbye, and invalid input messages.
	public abstract class ConsoleUIBase(IValidation validation, IHighScoreManager highScoreManager) : IConsoleUI
	{
		protected readonly IValidation _validation = validation;

		protected readonly IHighScoreManager _highScoreManager = highScoreManager;

		// Abstract methods for welcome and goodbye messages, to be implemented by derived classes for specific games.
		public abstract string GetWelcomeMessageFormat(GameTypes gameType);

		public abstract string GetGoodbyeMessageFormat(GameTypes gameType);

		// Gets the user's name with validation, prompting the user until a valid name is entered.
		public string GetUserName()
		{
			Console.Clear();
			string userName;
			do
			{
				Console.Write(PromptMessages.UserNamePrompt);
				userName = Console.ReadLine()!.Trim();
				Console.WriteLine(_validation.ValidateUserName(userName));
			}
			while (!_validation.IsValidUserName(userName));
			Console.Clear();
			return userName;
		}

		// Displays the welcome message, customized based on whether it’s a new or returning user.
		public void DisplayWelcomeMessage(GameTypes gameType, string userName, bool isNewGame)
		{
			string welcomeMessage = GetWelcomeMessageFormat(gameType);
			Console.WriteLine(isNewGame
				? string.Format(welcomeMessage, userName)
				: string.Format(GameMessages.WelcomeBackMessageFormat, userName));
		}

		// Displays the secret number to the user in practice mode.
		public void DisplaySecretNumberForPractice(string secretNumber)
		{
			Console.WriteLine($"{string.Format(GameMessages.SecretNumberPracticeMessage, secretNumber)}\n");
		}

		// Repeatedly prompts the user for a valid guess, using the validation service to verify the input.
		public string GetValidGuessFromUser(GameTypes gameType)
		{
			string guess;
			bool isValidInput;

			do
			{
				guess = GetInputFromUser();
				isValidInput = _validation.IsInputValid(gameType, guess);

				if (!isValidInput)
				{
					DisplayInvalidInputMessage();
				}
			} while (!isValidInput);

			return guess;
		}

		// Abstract method to display invalid input message, implementation provided by derived classes.
		public abstract void DisplayInvalidInputMessage();

		// Prompts the user for input and returns the trimmed result.
		public string GetInputFromUser()
		{
			Console.Write(PromptMessages.GuessPrompt);
			return Console.ReadLine()!.Trim();
		}

		// Displays feedback after a user's guess, providing hints or indicating matches found.
		public void DisplayGuessFeedback(string guessFeedback)
		{
			Console.WriteLine($"{GameMessages.FeedbackPrefix}{(guessFeedback == "," ? GameMessages.NoMatchesFoundMessage : guessFeedback)}\n");
		}

		// Displays a congratulatory message when the user guesses the correct secret number.
		public void DisplayCorrectMessage(string secretNumber, int numberOfGuesses)
		{
			Console.WriteLine(string.Format(
				GameMessages.CorrectGuessMessageFormat,
				secretNumber,
				numberOfGuesses,
				numberOfGuesses == 1 ? Plurals.GuessSingular : Plurals.GuessPlural
			));
		}

		// Displays the high score list, including fetching and sorting the high scores.
		public void DisplayHighScoreList(string currentUserName)
		{
			var results = _highScoreManager.ReadHighScoreResultsFromFile();
			_highScoreManager.SortHighScoreList(results);
			RenderHighScoreList(results, currentUserName);
		}

		// Renders the high score list on the console, highlighting the current user.
		public void RenderHighScoreList(List<IPlayerData> results, string currentUserName)
		{
			var (maxUserNameLength, totalWidth) = CalculateDisplayDimensions(results);
			DisplayHighScoreListHeader(maxUserNameLength, totalWidth);
			PrintHighScoreResults(results, currentUserName, maxUserNameLength);
			Console.WriteLine(FormatUtils.CreateSeparatorLine(totalWidth));
		}

		// Calculates the maximum username length and total display width for formatting.
		// Usernames shorter than six characters are treated as six characters long
		// to maintain consistent column width in the display.
		// This ensures that the table formatting remains aligned and visually appealing,
		// even for users with short usernames.
		public (int maxUserNameLength, int totalWidth) CalculateDisplayDimensions(List<IPlayerData> results)
		{
			int maxUserNameLength = Math.Max(results.Max(p => p.UserName.Length), 6);
			int totalWidth = FormattingConstants.RankColumnWidth + maxUserNameLength + FormattingConstants.GamesPlayedColumnWidth + FormattingConstants.AverageGuessesColumnWidth + FormattingConstants.Padding;
			return (maxUserNameLength, totalWidth);
		}

		// Displays the header for the high score list, centered based on the total width.
		public void DisplayHighScoreListHeader(int maxUserNameLength, int totalWidth)
		{
			int leftPadding = (totalWidth - HighScoreHeaders.HighScoreHeader.Length) / 2;

			// Formats the header row with dynamic column widths.
			string headerRowFormat = string.Format(
			   "{0,-" + FormattingConstants.RankColumnWidth + "} " +
			   "{1,-" + maxUserNameLength + "} " +
			   "{2," + FormattingConstants.GamesPlayedColumnWidth + "} " +
			   "{3," + FormattingConstants.AverageGuessesColumnWidth + "}",
			   HighScoreHeaders.RankHeader,
			   HighScoreHeaders.PlayerHeader,
			   HighScoreHeaders.GamesHeader,
			   HighScoreHeaders.AverageGuessesHeader
		   );

			// Displays the formatted header and separator lines.
			string headerFormat = $"{new string(' ', leftPadding)}{HighScoreHeaders.HighScoreHeader}\n" +
								  $"{FormatUtils.CreateSeparatorLine(totalWidth)}\n" +
								  $"{headerRowFormat}\n" +
								  $"{FormatUtils.CreateSeparatorLine(totalWidth)}";

			Console.WriteLine(headerFormat);
		}

		// Prints each high score result, highlighting the current user's score.
		public void PrintHighScoreResults(List<IPlayerData> results, string currentUserName, int maxUserNameLength)
		{
			int rank = 1;
			foreach (var player in results)
			{
				bool isCurrentUser = player.UserName.Equals(currentUserName, StringComparison.OrdinalIgnoreCase);
				Console.ForegroundColor = isCurrentUser ? ConsoleColor.Green : ConsoleColor.White;
				Console.Write($"{rank,-FormattingConstants.RankColumnWidth} ");
				Console.WriteLine($"{player.UserName.PadRight(maxUserNameLength)} {player.TotalGamesPlayed,FormattingConstants.GamesPlayedColumnWidth} {player.CalculateAverageGuesses(),FormattingConstants.AverageGuessesColumnWidth:F2}");

				Console.ResetColor();
				rank++;
			}
		}

		// Sets the console color based on whether the player is the current user.
		public void SetConsoleColor(bool isCurrentUser)
		{
			Console.ForegroundColor = isCurrentUser ? ConsoleColor.Green : ConsoleColor.White;
		}

		// Displays the rank in the high score list.
		public void DisplayRank(int rank)
		{
			Console.Write($"{rank,-FormattingConstants.RankColumnWidth}");
		}

		// Displays the player's high score data.
		public void DisplayPlayerData(IPlayerData player, bool isCurrentUser, int maxUserNameLength)
		{
			Console.WriteLine($" {player.UserName.PadRight(maxUserNameLength)} {player.TotalGamesPlayed,FormattingConstants.GamesPlayedColumnWidth} {player.CalculateAverageGuesses(),FormattingConstants.AverageGuessesColumnWidth:F2}");
		}

		// Prompts the user to continue the game, repeating until a valid yes/no input is provided.
		public bool AskToContinue()
		{
			while (true)
			{
				Console.Write(PromptMessages.PlayAgainPrompt);
				string answer = Console.ReadLine()!.ToLower().Trim();
				if (answer == UserInputConstants.YesInput)
				{
					Console.Clear();
					return true;
				}
				else if (answer == UserInputConstants.NoInput)
				{
					return false;
				}
				else
				{
					Console.WriteLine(UserInteractionMessages.InvalidPlayAgainResponse);
				}
			}
		}

		// Displays the goodbye message when the user chooses to quit and
		// then waits for the user to press a key before closing.
		public void DisplayGoodbyeMessage(GameTypes gameType, string userName)
		{
			string goodbyeMessageFormat = GetGoodbyeMessageFormat(gameType);
			Console.WriteLine($"\n{string.Format(goodbyeMessageFormat, userName)}\n");
			ConsoleUtils.WaitForUserToContinue(PromptMessages.PressAnyKeyToReturn);
		}
	}
}