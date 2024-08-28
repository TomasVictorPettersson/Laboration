using ConsoleUI.Interfaces;
using ConsoleUI.Utils;
using GameLogic.Interfaces;
using GameResources.Constants;
using GameResources.Enums;
using HighScoreManagement.Interfaces;
using Validation.Interfaces;

namespace GameLogic.Implementations
{
	// Abstract base class for game logic, providing common functionality for different game types.
	// Implements the IGameLogic interface and defines the core game loop and helper methods.
	public abstract class GameLogicBase(IHighScoreManager highScoreManager, IConsoleUI consoleUI, IValidation validation) : IGameLogic
	{
		private readonly IHighScoreManager _highScoreManager = highScoreManager;
		private readonly IConsoleUI _consoleUI = consoleUI;
		private readonly IValidation _validation = validation;

		// Starts the game by displaying a welcome message, generating a secret number,
		// and running the main game loop. Handles exceptions and displays error messages.
		public void PlayGame(string userName, bool isNewGame)
		{
			try
			{
				_consoleUI.DisplayWelcomeMessage(GetGameType(), userName, isNewGame);
				string secretNumber = MakeSecretNumber();
				ConsoleUtils.WaitForUserToContinue(isNewGame ? $"\n{PromptMessages.InstructionsReadMessage}" : $"\n{PromptMessages.StartPlayingPrompt}");

				// Optionally display the secret number for practice mode.
				_consoleUI.DisplaySecretNumberForPractice(secretNumber);

				PlayGameLoop(secretNumber, userName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error playing game: {ex.Message}");
				throw;
			}
		}

		// Abstract method for generating a secret number, to be implemented by derived classes.
		public abstract string MakeSecretNumber();

		// Main game loop that processes user guesses until the correct guess is made.
		// Ends the game once the correct guess is found.
		public void PlayGameLoop(string secretNumber, string userName)
		{
			int numberOfGuesses = 0;
			bool isGuessCorrect = false;

			while (!isGuessCorrect)
			{
				isGuessCorrect = HandleUserGuess(secretNumber, ref numberOfGuesses);
			}

			EndGame(secretNumber, userName, numberOfGuesses);
		}

		// Handles user input for a guess and processes it.
		public bool HandleUserGuess(string secretNumber, ref int numberOfGuesses)
		{
			string guess = _consoleUI.GetValidGuessFromUser(GetGameType());
			return ProcessGuess(secretNumber, guess, ref numberOfGuesses);
		}

		// Processes the user's guess, generates feedback, and updates the guess count.
		public bool ProcessGuess(string secretNumber, string guess, ref int numberOfGuesses)
		{
			string feedback = GenerateFeedback(secretNumber, guess);
			_consoleUI.DisplayGuessFeedback(feedback);
			numberOfGuesses++;
			return _validation.IsCorrectGuess(guess, secretNumber);
		}

		// Ends the game by saving the result, displaying the correct number and number of guesses,
		// showing the high score list, and asking the user if they want to continue.
		public void EndGame(string secretNumber, string userName, int numberOfGuesses)
		{
			_highScoreManager.SaveResult(userName, numberOfGuesses);
			_consoleUI.DisplayCorrectMessage(secretNumber, numberOfGuesses);
			ConsoleUtils.WaitForUserToContinue(PromptMessages.FinishGamePrompt);
			_consoleUI.DisplayHighScoreList(userName);
			ConsoleUtils.WaitForUserToContinue(PromptMessages.ContinuePrompt);
		}

		// Abstract method to generate feedback based on the game rules and must be implemented by derived classes
		// The feedback should provide information about the number of bulls and cows in the user's guess.
		public abstract string GenerateFeedback(string secretNumber, string guess);

		// Abstract method to count the number of cows (correct digits in correct positions).
		// To be implemented by derived classes based on specific game rules.
		public abstract int CountBulls(string secretNumber, string guess);

		// Abstract method to count the number of cows (correct digits in incorrect positions).
		// To be implemented by derived classes based on specific game rules.
		public abstract int CountCows(string secretNumber, string guess);

		// Abstract method to get the game type (e.g., MasterMind, BullsAndCows).
		// To be implemented by derived classes to specify the type of game.
		public abstract GameTypes GetGameType();
	}
}