using Laboration.ConsoleUI.Interfaces;
using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.PlayerData.Interfaces;
using Laboration.Validation.Interfaces;

namespace Laboration.ConsoleUI.Implementations
{
	// Handles user interactions for the Bulls and Cows game in a console application.
	public class GameConsoleUI(IValidation validation, IHighScoreManager highScoreManager) : IConsoleUI
	{
		private readonly IValidation _validation = validation;
		private readonly IHighScoreManager _highScoreManager = highScoreManager;

		// Prompts the user to enter their username, validating its length.
		public string GetUserName()
		{
			Console.Clear();
			string userName;
			do
			{
				Console.Write(UserInteractionMessages.UserNamePrompt);
				userName = Console.ReadLine()!.Trim();
				Console.WriteLine(_validation.ValidateUserName(userName));
			}
			while (!_validation.IsValidUserName(userName));
			Console.Clear();
			return userName;
		}

		// Displays a personalized welcome message to the player based on the game type.
		// Shows a detailed welcome message if it's a new game, or a brief welcome back message if the player has played before.
		public void DisplayWelcomeMessage(GameTypes gameType, string userName, bool isNewGame)
		{
			string welcomeMessage = GetWelcomeMessageFormat(gameType);

			Console.WriteLine(
				isNewGame
					? string.Format(welcomeMessage, userName)
					: string.Format(GameMessages.WelcomeBackMessageFormat, userName)
			);
		}

		// Returns the appropriate welcome message format based on the game type.
		public string GetWelcomeMessageFormat(GameTypes gameType)
		{
			return gameType switch
			{
				GameTypes.BullsAndCows => GameMessages.BullsAndCowsWelcomeMessageFormat,
				GameTypes.MasterMind => GameMessages.MasterMindWelcomeMessageFormat,
				_ => throw new ArgumentException("Invalid game type", nameof(gameType))
			};
		}

		// Displays the secret number for practice mode.
		public void DisplaySecretNumberForPractice(string secretNumber)
		{
			try
			{
				Console.WriteLine($"{string.Format(GameMessages.SecretNumberPracticeMessage, secretNumber)}\n");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error displaying secret number: {ex.Message}");
				throw;
			}
		}

		// Prompts the user to enter a valid 4-digit guess based on the game type.
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
					// Display appropriate message based on game type
					switch (gameType)
					{
						case GameTypes.MasterMind:
							Console.WriteLine(UserInteractionMessages.MasterMindInvalidInputMessage);
							break;

						case GameTypes.BullsAndCows:
							Console.WriteLine(UserInteractionMessages.BullsAndCowsInvalidInputMessage);
							break;
					}
				}
			} while (!isValidInput);

			return guess;
		}

		// Gets input from the user.
		public string GetInputFromUser()
		{
			Console.Write(UserInteractionMessages.GuessPrompt);
			return Console.ReadLine()!.Trim();
		}

		// Displays feedback for the player's guess.
		public void DisplayGuessFeedback(string guessFeedback)
		{
			Console.WriteLine($"{GameMessages.FeedbackPrefix}{(guessFeedback == "," ? GameMessages.NoMatchesFoundMessage : guessFeedback)}\n");
		}

		// Displays a message indicating the correct number and number of guesses taken.
		public void DisplayCorrectMessage(string secretNumber, int numberOfGuesses)
		{
			Console.WriteLine(string.Format(
				GameMessages.CorrectGuessMessageFormat,
				secretNumber,
				numberOfGuesses,
				numberOfGuesses == 1 ? Plurals.GuessSingular : Plurals.GuessPlural
			));
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

		// Displays the header for the high score list with proper formatting.
		public void DisplayHighScoreListHeader(int maxUserNameLength, int totalWidth)
		{
			// Calculate left padding for centered header
			int leftPadding = (totalWidth - HighScoreHeaders.HighScoreHeader.Length) / 2;

			// Create formatted header row
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

			// Construct the complete header format
			string headerFormat = $"{new string(' ', leftPadding)}{HighScoreHeaders.HighScoreHeader}\n" +
								  $"{CreateSeparatorLine(totalWidth)}\n" +
								  $"{headerRowFormat}\n" +
								  $"{CreateSeparatorLine(totalWidth)}";

			// Output the header format to the console
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
			Console.Write($"{rank,-FormattingConstants.RankColumnWidth}");
		}

		// Displays detailed player data, with special formatting for the current user.
		public void DisplayPlayerData(IPlayerData player, bool isCurrentUser, int maxUserNameLength)
		{
			Console.WriteLine($" {player.UserName.PadRight(maxUserNameLength)} {player.TotalGamesPlayed,FormattingConstants.GamesPlayedColumnWidth} {player.CalculateAverageGuesses(),FormattingConstants.AverageGuessesColumnWidth:F2}");
		}

		// Asks the user if they want to continue playing or exit.
		public bool AskToContinue()
		{
			while (true)
			{
				Console.Write(UserInteractionMessages.PlayAgainPrompt);
				string answer = Console.ReadLine()!.ToLower().Trim();
				switch (answer)
				{
					case UserInputConstants.YesInput:
						Console.Clear();
						return true;

					case UserInputConstants.NoInput:
						return false;

					default:
						Console.WriteLine(UserInteractionMessages.InvalidPlayAgainResponse);
						break;
				}
			}
		}

		// Method to display a personalized goodbye message based on the game type
		public void DisplayGoodbyeMessage(GameTypes gameType, string userName)
		{
			string goodbyeMessageFormat = GetGoodbyeMessageFormat(gameType);

			Console.WriteLine(string.Format(goodbyeMessageFormat, userName));
			WaitForUserToContinue(PromptMessages.CloseWindowPrompt);
		}

		// Returns the appropriate goodbye message format based on the game type.
		public string GetGoodbyeMessageFormat(GameTypes gameType)
		{
			return gameType switch
			{
				GameTypes.BullsAndCows => GameMessages.BullsAndCowsGoodbyeMessageFormat,
				GameTypes.MasterMind => GameMessages.MasterMindGoodbyeMessageFormat,
				_ => throw new ArgumentException("Invalid game type", nameof(gameType))
			};
		}
	}
}